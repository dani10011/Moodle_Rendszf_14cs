
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

  


  //Új kurzus létrehozása (csak tanár tudja)
function kurzusLetrehozas() {
    const dataDisplay = document.getElementById("dataDisplay");
    dataDisplay.innerHTML = '';
  
    // Elkészíti a formot
    const form = document.createElement("form");
    form.className = "osszForm";
  
    // Input mezőket csinál
    const inputName = document.createElement("input");
    inputName.type = "text";
    inputName.className = "formTexts";
    inputName.placeholder = "Kurzus neve";
  
    const inputCode = document.createElement("input");
    inputCode.type = "text";
    inputCode.className = "formTexts";
    inputCode.placeholder = "Kurzus kódja";
  
    const inputCredit = document.createElement("input");
    inputCredit.type = "text";
    inputCredit.className = "formTexts";
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
  async function esemenyForm(course_id) {
    //course_id!!!!!!!!!!!
  
    const dataDisplay = document.getElementById("dataDisplay");
    dataDisplay.innerHTML = '';
  
    // Elkészíti a formot
    const form = document.createElement("form");
    form.className = "osszForm";
  
    // Input mezőket csinál
    //name
    const inputName = document.createElement("input");
    inputName.type = "text";
    inputName.id = "nameId";
    inputName.className = "formTexts";
    inputName.placeholder = "Esemény neve";
  
    //description
    const inputDesc = document.createElement("input");
    inputDesc.type = "text";
    inputDesc.id = "descId";
    inputDesc.className = "formTexts";
    inputDesc.placeholder = "Esemény leírása";
  
    // Létrehozás gomb
    const submitButton = document.createElement("button");
    submitButton.type = "button"; // Change the button type to prevent form submission
    submitButton.textContent = "Esemény létrehozása";
    submitButton.addEventListener('click', async () => {
        var name = document.getElementById("nameId").value;
        var description = document.getElementById("descId").value;
    
        const AddEvent = {
            course_id: course_id,
            name: name,
            description: description
        };
    
        try {
            const response = await fetch('https://localhost:7090/api/Course/AddEvent', {
                method: 'POST',
                body: JSON.stringify(AddEvent),
                headers: { 'Content-Type': 'application/json' }
            });
    
            if (!response.ok) {
                const message = await response.text();
                alert(message);
            } else {
                const data = await response.json();
                const message = data.message;
                alert(message);
                console.log(message);
            }
        } catch (error) {
            console.error('Error occurred:', error);
            alert('Hiba történt a szerverrel való kommunikáció során.');
        }
    });
  
    // Hozzáadás a form-hoz
    form.appendChild(inputName);
    form.appendChild(inputDesc);
    form.appendChild(submitButton);
    dataDisplay.appendChild(form);
  }
  