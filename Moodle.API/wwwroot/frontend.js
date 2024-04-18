
/*function bejelentkezes(){
  
        var felhasznalomezo = document.getElementById('felhasznalonev');
        var felhasznalonev = felhasznalomezo.value;
        var jelszomezo = document.getElementById('jelszo');
        var jelszo = jelszomezo.value;
        
        console.log(felhasznalonev);
        console.log(jelszo);
    
        // Submit the form
        document.getElementById('loginForm').submit();   
}*/


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
    const message = await response.text();
    alert(message);
    window.location.href = 'mainPage.html'
  }
}

function osszLista() {
  const url = "https://localhost:7090/api/Course/allcourses";
  fetch(url) //kérés küldése
    .then(response => { //ellenőrzi a választ
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      return response.json(); //jsonné alakítja a választ, majd továbbadja a következő then-nek
    })
    .then(data => {
      //console.log(data); // Logolja a Json file tartalmát a konzolra (TESZTELÉSHEZ)
      const dataDisplay = document.getElementById("dataDisplay"); 
      dataDisplay.innerHTML = '';
      const ul = document.createElement('ul');
      data.forEach(item => {
        const li = document.createElement('li'); // elemenként egy li
        li.textContent = `${item.name} (${item.code}, ${item.department}), kredit: ${item.credit}`;

        li.addEventListener('click', () => {
          hallgatoEsemeny(dataDisplay);
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
  const url = "https://localhost:7090/api/Course/courseid";
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
        li.textContent = `${item.name} (${item.code}, ${item.department}), kredit: ${item.credit}`;
        li.addEventListener('click', () => {
          hallgatoEsemeny(dataDisplay);
        });
        ul.appendChild(li);
      });
      dataDisplay.appendChild(ul);
    })
    .catch(error => {
      console.error('There was a problem with the fetch operation:', error);
    });

}






/*
function lekeresteszt(){
    fetch('/probajson') // Assuming that your API endpoint is located at '/probajson'
  .then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  })
  .then(data => {
    console.log(data); // Log the content of the JSON file to the console
  })
  .catch(error => {
    console.error('There was a problem with the fetch operation:', error);
  });

}*/


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
      uniqueDepartments.add(item.department); 
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

      const filteredData = data.filter(item => item.department === selectedDepartment);

      // Szűrt elemek kilistázása
      if (selectedDepartment == 'Összes kurzus listázása') {
        osszLista();
      }
      else {
        filteredData.forEach(item => {
          const li = document.createElement('li'); // elemenként egy li
          li.textContent = `${item.name} (${item.code}, ${item.department}), kredit: ${item.credit}`;
          li.addEventListener('click', () => {
            hallgatoEsemeny(dataDisplay);
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






async function fetchDataResztvevoListazas(jelenlegiKurzus) {
  const url = "https://localhost:7090/api/Course/allcourses";
/*

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
*/



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

function myFunction() {
  document.getElementById("myDropdown").classList.toggle("show");
}

function filterFunction() {
  const input = document.getElementById("myInput");
  const filter = input.value.toUpperCase();
  const div = document.getElementById("myDropdown");
  const a = div.getElementsByTagName("a");
  for (let i = 0; i < a.length; i++) {
    txtValue = a[i].textContent || a[i].innerText;
    if (txtValue.toUpperCase().indexOf(filter) > -1) {
      a[i].style.display = "";
    } else {
      a[i].style.display = "none";
    }
  }
}



function hallgatoEsemeny(dataDisplay) { //megjeleníti a menüt, miután a kurzusok vaalmielyikére kattintunk
  const div = document.createElement('div');

  dataDisplay.innerHTML = '';

  const esemenyek = document.createElement('button');
  esemenyek.textContent = 'Események';
  esemenyek.id = 'esemenyekLista';
  dataDisplay.appendChild(esemenyek);
  esemenyek.addEventListener('click', () => { //funkcio rendelése a gombhoz
    console.log("proba");
  });

  dataDisplay.appendChild(div);

  const hallgatok = document.createElement('button');
  hallgatok.id = 'hallgatokLista';
  hallgatok.textContent = 'Hallgatók';
  dataDisplay.appendChild(hallgatok);

  dataDisplay.appendChild(div);

  const vissza = document.createElement('button');
  vissza.textContent = 'Vissza';
  vissza.id = 'vissza';
  dataDisplay.appendChild(vissza);
  vissza.addEventListener('click', () => { //funkcio rendelése a gombhoz
    osszLista();
  });
}

function kijelentkezes(){
  window.location.href = 'frontend.html';
}