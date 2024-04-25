//bejelentkezési funkció
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
  } 
  else {
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




//összes kurzus kilistázása
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




//saját kurzusok kiiratása
function sajatlista() { 
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




//tanszék szerinti szűrés
async function fetchDataTanszek() { 
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






//megjeleníti a menüt, miután a kurzusok valamielyikére kattintunk
function hallgatoEsemeny(id) { 
  const div = document.createElement('div');

  dataDisplay.innerHTML = '';

  const esemenyek = document.createElement('button');
  esemenyek.textContent = 'Események';
  esemenyek.id = 'esemenyekLista';
  dataDisplay.appendChild(esemenyek);
  esemenyek.addEventListener('click', () => { //funkcio rendelése a gombhoz
      esemenyListazas(id); //meghívja az esemenyeket
  });

  dataDisplay.appendChild(div);

  const hallgatok = document.createElement('button');
  hallgatok.id = 'hallgatokLista';
  hallgatok.textContent = 'Hallgatók';
  dataDisplay.appendChild(hallgatok);
  hallgatok.addEventListener('click', () => { //funkcio rendelése a gombhoz
    hallgatoListazas(id); //megívja az esemenyeket
  });
  dataDisplay.appendChild(div);
}





//halgatók kilistázása egy adott kurzuson
async function hallgatoListazas(aktualisId) {
  var id = aktualisId;
    var url = "https://localhost:7090/api/Course/enrolled?id=" + id;
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
        li.textContent = `${item.Name} (${item.Username})`;
        ul.appendChild(li);
      });
      dataDisplay.appendChild(ul);
    })
    .catch(error => {
      console.error('There was a problem with the fetch operation:', error);
    });
}






//kurzusfelvétel, csak tanár tudja
function kurzusFelvetel() {

  const retrievedData = sessionStorage.getItem('currentUserId');
  const url = "https://localhost:7090/api/Course/notincourseid?id=" + retrievedData;
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




//új esemény létrehozása (csak tanár)
async function esemenyLetrehozas() {
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
        console.log(item.Id);
        esemenyForm(item.Id);
      });
      ul.appendChild(li);
    });
    dataDisplay.appendChild(ul);
  })
  .catch(error => {
    console.error('There was a problem with the fetch operation:', error);
  });
}


//megjeleníti a form-ot új esemény létrehozásáshoz
async function esemenyForm(course_id){
  //course_id!!!!!!!!!!!

  const dataDisplay = document.getElementById("dataDisplay");
  dataDisplay.innerHTML = '';

  // Elkészíti a formot
  const form = document.createElement("form");
  form.className="osszForm";

  // Input mezőket csinál
  //name
  const inputName = document.createElement("input");
  inputName.type = "text";
  inputName.id = "nameId";
  inputName.className = "formClass";
  inputName.placeholder = "Esemény neve";

  //description
  const inputDesc = document.createElement("input");
  inputDesc.type = "text";
  inputDesc.id = "descId";
  inputDesc.className = "formClass";
  inputDesc.placeholder = "Esemény leírása";

  // Létrehozás gomb
  const submitButton = document.createElement("button");
  submitButton.type = "submit";
  submitButton.textContent = "Esemény létrehozása";
  submitButton.addEventListener('click', async () => {
  var name = document.getElementById("nameId").value;
  var description = document.getElementById("descId").value;

  const AddEvent = {
    course_id: course_id,
    name: name,
    description: description
  };
  const response = await fetch('https://localhost:7090/api/Course/AddEvent', { 
    method: 'POST',
    body: JSON.stringify(AddEvent),
    headers: { 'Content-Type': 'application/json' }
  });

  if (!response.ok) {
    const message = await response.text(); //MEGOL(DANI)!!!!!
    alert(message);
  } 
  else {
    const data = await response.json();
    const message = data.message;
    console.log(message);
    alert(message);
    alert("Sikeres lefutás");
  }
})

  // Hozzáadás a form-hoz
  form.appendChild(inputName);
  form.appendChild(inputDesc);
  form.appendChild(submitButton);
  dataDisplay.appendChild(form);
}



async function esemenyListazas(aktualisId) {
  var id = aktualisId;
    var url = "https://localhost:7090/api/Course/event?id=" + id;
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
        li.textContent = `${item.Name}: ${item.Description}`;
        ul.appendChild(li);
      });
      dataDisplay.appendChild(ul);
    })
    .catch(error => {
      console.error('There was a problem with the fetch operation:', error);
    });
}





function kijelentkezes() {
  window.location.href = 'frontend.html';
}
