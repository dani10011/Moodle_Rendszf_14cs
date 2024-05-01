



async function kurzusLetrehozas() {
  const retrievedData = sessionStorage.getItem('currentUserId');
  const dataDisplay = document.getElementById("dataDisplay");
  dataDisplay.innerHTML = '';

  // Create the form
  const form = document.createElement("form");
  form.className = "osszForm";

  // Input fields for course details
  const inputName = createInput("text", "inputName", "formTexts", "Kurzus neve");
  const inputCode = createInput("text", "inputCode", "formTexts", "Kurzus kódja");
  const inputCredit = createInput("text", "inputCredit", "formTexts", "Kredit");
  const inputDepartment = createInput("text", "inputDepartment", "formTexts", "Tanszék");

  // Label for accepted degrees
  const felirat = document.createElement("label");
  felirat.textContent = "Elfogadott karok:";
  felirat.style.fontSize = "18px";
  felirat.style.paddingBottom = "1%";

  const feliratDiv = document.createElement("div");

  //CHECKBOXOK BEOLVASÁSA 
  const url = "https://localhost:7090/api/Course/AllDegrees";

try {
  const response = await fetch(url);
  if (!response.ok) {
    throw new Error('Network response was not ok');
  }
  const data = await response.json();

  console.log(data); 

  const departmentList = document.getElementById("departmentList");
  departmentList.innerHTML = '';

  degreeList = new Set();

  data.forEach(item => { //összes degreet hozzáadjuk
    degreeList.add(item.Name);
  });

  for (const degree of degreeList) {
    if(degree != "Tanár"){
    const item = data.find(item => item.Name === degree);
    const checkbox = document.createElement("input");
    checkbox.type = "checkbox";
    checkbox.id = `degree-${item.Id}`;
    checkbox.value = item.Name;

    const checkboxLabel = document.createElement("label");
    checkboxLabel.textContent = item.Name;
    checkboxLabel.htmlFor = checkbox.id;
    checkboxLabel.style.display = "block"; // add this line to make each label appear on a new line

    checkboxLabel.appendChild(checkbox); // append checkbox to label
    feliratDiv.appendChild(checkboxLabel); // append label to div
    }
  }

} catch (error) {
  console.error('There was a problem with the fetch operation:', error);
}


  // Submit button
  const submitButton = document.createElement("button");
  submitButton.type = "submit";
  submitButton.textContent = "Kurzus létrehozása";

  submitButton.addEventListener('click', async () => {
    event.preventDefault();

    const name = document.getElementById("inputName").value;
    const code = document.getElementById("inputCode").value;
    const credit = document.getElementById("inputCredit").value;
    const department = document.getElementById("inputDepartment").value;

    // Get selected degrees
    const selectedDegrees = [];
    for (const degree of degrees) {
      const checkbox = document.getElementById(`degree-${degree.Id}`);
      if (checkbox.checked) {
        selectedDegrees.push(degree.Id);
      }
    }

    const AddCourse = {
      name: name,
      code: code,
      credit: credit,
      department: department,
      userId: retrievedData,
      selectedDegrees: selectedDegrees
    };
      
    // ... rest of the fetch logic for submitting data ...
  });

  // Add elements to the form
  form.appendChild(inputName);
  form.appendChild(inputCode);
  form.appendChild(inputCredit);
  form.appendChild(inputDepartment);
  form.appendChild(felirat);
  form.appendChild(feliratDiv);
  form.appendChild(submitButton);

  dataDisplay.appendChild(form);
}

// Helper function to create input elements
function createInput(type, id, className, placeholder) {
  const input = document.createElement("input");
  input.type = type;
  input.id = id;
  input.className = className;
  input.placeholder = placeholder;
  return input;
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
