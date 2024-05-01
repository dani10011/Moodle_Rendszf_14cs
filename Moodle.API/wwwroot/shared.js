
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
          li.textContent = `${item.Name} (${item.UserName})`;
          ul.appendChild(li);
        });
        dataDisplay.appendChild(ul);
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
  }

  



  
//események kilistázása az adott kurzushoz
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