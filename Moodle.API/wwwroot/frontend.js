
async function bejelentkezes() { //aszinkron: várhat egy művelet befejezésére
  var username = document.getElementById("username").value;
  var password = document.getElementById("password").value;

  const loginData = {
    Username: username,
    Password: password
  };

  const response = await fetch('https://localhost:7090/api/Authentication/login', { //elküldi a felhasználónév jelszó párost, await megállítja a funkciót amíg válaszra vár
    method: 'POST',
    body: JSON.stringify(loginData),
    headers: { 'Content-Type': 'application/json' }
  });

  if (!response.ok) { //ellenőrzi a login válaszát, ha Ok, átvisz a mainPage-re
    const message = await response.text();
    alert(message);
  } else {
    const data = await response.json(); // Parse the JSON response
    const message = data.message;
    const userId = data.userId;
    const role = data.role;
    //const token = data.token;
    
    sessionStorage.setItem('currentUserId', userId);
      
    currentUserId = userId;
    console.log("Id: ", currentUserId);
    console.log("Login successful:", message);

    alert(message);
    
    if(role == 'tanár'){
      window.location.href = 'mainPage_teacher.html';
    }
    else if(role == 'diák'){
      window.location.href = 'mainPage_student.html';
    }
  }

}

function osszLista() {
  const url = "https://localhost:7090/api/Course/allcourses";
    const retrievedData = sessionStorage.getItem('currentUserId');
    console.log(retrievedData);
  fetch(url) //kérés küldése
    .then(response => { //ellenőrzi a választ
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      return response.json(); //jsonné alakítja a választ, majd továbbadja a következő then-nek
    })
    .then(data => {
      console.log(data); // Logolja a Json file tartalmát a konzolra (TESZTELÉSHEZ)
      const dataDisplay = document.getElementById("dataDisplay");
      dataDisplay.innerHTML = '';
      const ul = document.createElement('ul');
      data.forEach(item => {
        const li = document.createElement('li'); // elemenként egy li
        li.textContent = `${item.Name} (${item.Code}, ${item.Department}), kredit: ${item.Credit}`;

        li.addEventListener('click', () => {
          hallgatoEsemeny(item.Id);
          const vissza = document.createElement('button');
          vissza.textContent = 'Vissza';
          vissza.id = 'vissza';
          dataDisplay.appendChild(vissza);
          vissza.addEventListener('click', () => { //funkcio rendelése a gombhoz
            osszLista();

          });
        });

        ul.appendChild(li);

      });
      dataDisplay.appendChild(ul);
    })
    .catch(error => {
      console.error('There was a problem with the fetch operation:', error);
    });

}



function sajatlista() { //lényegében ugyanaz, mint az összlista, csak más függvényt hív meg
    const retrievedData = sessionStorage.getItem('currentUserId');
    var url = "https://localhost:7090/api/Course/courseid?id=" + retrievedData;
  fetch(url)
    .then(response => {
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      return response.json();
    })
    .then(data => {
      const dataDisplay = document.getElementById("dataDisplay");
      dataDisplay.innerHTML = '';
      const ul = document.createElement('ul');
      data.forEach(item => {
        const li = document.createElement('li'); // elemenként egy li
        li.textContent = `${item.Name} (${item.Code}, ${item.Department}), kredit: ${item.Credit}`;
        li.addEventListener('click', () => {
          hallgatoEsemeny(item.Id);
          const vissza = document.createElement('button');
          vissza.textContent = 'Vissza';
          vissza.id = 'vissza';
          dataDisplay.appendChild(vissza);
          vissza.addEventListener('click', () => { //funkcio rendelése a gombhoz
            sajatlista();

          });
        });
        ul.appendChild(li);
      });
      dataDisplay.appendChild(ul);
    })
    .catch(error => {
      console.error('There was a problem with the fetch operation:', error);
    });

}





async function fetchDataTanszek() { //tanszék szerinti szűrés
  const url = "https://localhost:7090/api/Course/allcourses";

  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    const data = await response.json();

    console.log(data); //idáig megkapjuk az összes kurzust

    const departmentList = document.getElementById("departmentList");
    departmentList.innerHTML = '';


    const uniqueDepartments = new Set();

    uniqueDepartments.add('Összes kurzus listázása');
    data.forEach(item => { //össze stanszéket hozzáadjuk
      uniqueDepartments.add(item.Department);
    });

    for (const department of uniqueDepartments) {
      const option = document.createElement("option");
      option.text = department;
      departmentList.appendChild(option);
    }

    // Minden elem kiiratása    
    osszLista();

    departmentList.addEventListener("change", function () {
      const selectedDepartment = this.value;
      const dataDisplay = document.getElementById("dataDisplay");
      const ul = document.createElement('ul');
      dataDisplay.innerHTML = '';

      const filteredData = data.filter(item => item.Department === selectedDepartment);

      // Szűrt elemek kilistázása
      if (selectedDepartment == 'Összes kurzus listázása') {
        osszLista();
      }
      else {
        filteredData.forEach(item => {
          const li = document.createElement('li'); // elemenként egy li
          li.textContent = `${item.Name} (${item.Code}, ${item.Department}), kredit: ${item.Credit}`;
          li.addEventListener('click', () => {
            hallgatoEsemeny(item.Id);
            const vissza = document.createElement('button');
            vissza.textContent = 'Vissza';
            vissza.id = 'vissza';
            dataDisplay.appendChild(vissza);
            vissza.addEventListener('click', () => { //funkcio rendelése a gombhoz
              konkretDepartment(selectedDepartment);

            });
          });
          ul.appendChild(li);
        });
        dataDisplay.appendChild(ul);
      }

    });
  } catch (error) {
    console.error('There was a problem with the fetch operation:', error);
  }
}

/*
function konkretDepartment(actualDepartment) {
  const url = "https://localhost:7090/api/Course/allcourses";
  fetch(url)
    .then(response => {
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      return response.json();
    })
    .then(data => {
      console.log(data);
      const dataDisplay = document.getElementById("dataDisplay");
      dataDisplay.innerHTML = '';
      const ul = document.createElement('ul');

      //Létrehoz egy listát a megfelelő Departmentel rendelkező kurzusoknak
      const filteredData = data.filter(item => item.Department === actualDepartment);

      //Ebből listáz ki
      filteredData.forEach(item => {
        const li = document.createElement('li');
        li.textContent = `${item.Name} (${item.Code}, ${item.Department}), kredit: ${item.Credit}`;

        li.addEventListener('click', () => {
          hallgatoEsemeny(item.Id);
          const vissza = document.createElement('button');
          vissza.textContent = 'Vissza';
          vissza.id = 'vissza';
          dataDisplay.appendChild(vissza);
          vissza.addEventListener('click', () => {
            konkretDepartment(actualDepartment);
          });
        });

        ul.appendChild(li);
      });

      dataDisplay.appendChild(ul);
    })
    .catch(error => {
      console.error('There was a problem with the fetch operation:', error);
    });
}


*/






/*async function fetchDataResztvevoListazas(jelenlegiKurzus) {
  const url = "https://localhost:7090/api/Course/allcourses";
  
  
    var kurzus = jelenlegiKurzus;
  
    const response = await fetch('https://localhost:7090/api/Authentication/vmi', {
      method: 'POST',
      body: JSON.stringify(loginData),
      headers: { 'Content-Type': 'application/json' }
    });
  
    if (!response.ok) {
      const message = await response.text();
      alert(message);
    } else {
      const message = await response.text();
      alert(message);
      window.location.href = 'mainPage.html'
    }
  



  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    const data = await response.json();

    console.log(data);

    const studentList = document.getElementById("studentList");
    studentList.innerHTML = '';  // Clear any previous options

    // Create options for each course
    data.forEach(course => {
      const option = document.createElement("option");
      option.text = course.name + ' (' + course.code + ')';
      option.value = course; // Store entire course object in value
      studentList.appendChild(option);
    });

    studentList.addEventListener("change", function () {
      const selectedCourse = this.value; // Get the entire course object
      const dataDisplay = document.getElementById("dataDisplay");
      dataDisplay.innerHTML = ''; // Clear previous content

      // Check if a course is selected
      if (selectedCourse) {
        const enrolledStudents = selectedCourse.enrolled_students;
        dataDisplay.textContent = "Enrolled Students:";

        // Display enrolled students (optional)
        enrolledStudents.forEach(student => {
          const studentElement = document.createElement("p");
          studentElement.textContent = student;
          dataDisplay.appendChild(studentElement);
        });
      } else {
        dataDisplay.textContent = "Please select a course.";
      }
    });
  } catch (error) {
    console.error('There was a problem with the fetch operation:', error);
  }
}
*/





function hallgatoEsemeny(id) { //megjeleníti a menüt, miután a kurzusok valamielyikére kattintunk
  const div = document.createElement('div');

  dataDisplay.innerHTML = '';

  const esemenyek = document.createElement('button');
  esemenyek.textContent = 'Események';
  esemenyek.id = 'esemenyekLista';
  dataDisplay.appendChild(esemenyek);
  esemenyek.addEventListener('click', () => { //funkcio rendelése a gombhoz
      esemenyListazas();
  });

  dataDisplay.appendChild(div);

  const hallgatok = document.createElement('button');
  hallgatok.id = 'hallgatokLista';
  hallgatok.textContent = 'Hallgatók';
  dataDisplay.appendChild(hallgatok);
  hallgatok.addEventListener('click', () => { //funkcio rendelése a gombhoz
    hallgatoListazas(id);
  });
  dataDisplay.appendChild(div);


}

async function hallgatoListazas(aktualisId) {
  const Enrolled = {
    id: aktualisId
  };

  try {
    const response = await fetch('https://localhost:7090/api/Course/enrolled', {
      method: 'POST',
      body: JSON.stringify(Enrolled),
      headers: { 'Content-Type': 'application/json' }
    });

    if (!response.ok) {
      const message = await response.json();
      alert(message);
    } else {

      const data = await response.json();
      console.log(data);
      const dataDisplay = document.getElementById("dataDisplay");
      dataDisplay.innerHTML = '';
      const ul = document.createElement('ul');

      data.forEach(item => {
        const li = document.createElement('li');
        li.textContent = `${item.Name} (${item.UserName})`;



        ul.appendChild(li);
      });

      dataDisplay.appendChild(ul);
    }
  } catch (error) {
    console.error('There was a problem with the fetch operation:', error);
  }
}



function kurzusFelvetel() {
  const url = "https://localhost:7090/api/Course/notincourseid";
  fetch(url)
    .then(response => {
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      
      return response.json();
    })
    .then(data => {
      //console.log(data);
      const dataDisplay = document.getElementById("dataDisplay");
      dataDisplay.innerHTML = '';
      const ul = document.createElement('ul');
      data.forEach(item => {
        const li = document.createElement('li'); // elemenként egy li
        li.textContent = `${item.Name} (${item.Code}, ${item.Department}), kredit: ${item.Credit}`;
        li.addEventListener('click', () => {
            felvetel(item.Id);
          
        });
        ul.appendChild(li);
      });
      dataDisplay.appendChild(ul);
    })
    .catch(error => {
      console.error('There was a problem with the fetch operation:', error);
    });
  
}

function felvetel(courseId){

}

function kurzusLetrehozas() {
  const dataDisplay = document.getElementById("dataDisplay");
  dataDisplay.innerHTML = '';

  // Elkészíti a formot
  const form = document.createElement("form");
form.className="osszForm";

  // Input mezőket csinál
  const inputName = document.createElement("input");
  inputName.type = "text";
  inputName.className="formClass";
  inputName.placeholder = "Kurzus neve";

  const inputCode = document.createElement("input");
  inputCode.type = "text";
  inputCode.className="formClass";
  inputCode.placeholder = "Kurzus kódja";

  const inputCredit = document.createElement("input");
  inputCredit.type = "text";
  inputCredit.className="formClass";
  inputCredit.placeholder = "Kredit";

  // Létrehozás gomb
  const submitButton = document.createElement("button");
  submitButton.type = "submit";
  submitButton.textContent = "Kurzus létrehozása";

  // Hozzáadás a form-hoz
  form.appendChild(inputName);
  form.appendChild(inputCode);
  form.appendChild(inputCredit);

  form.appendChild(submitButton);

  dataDisplay.appendChild(form);
  
}

function esemenyLetrehozas() {

}

function esemenyListazas() {

}

function kijelentkezes() {
  window.location.href = 'frontend.html';
}
