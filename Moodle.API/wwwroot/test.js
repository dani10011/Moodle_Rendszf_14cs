
function bejelentkezes(){
  
        var felhasznalomezo = document.getElementById('felhasznalonev');
        var felhasznalonev = felhasznalomezo.value;
        var jelszomezo = document.getElementById('jelszo');
        var jelszo = jelszomezo.value;
        
        console.log(felhasznalonev);
        console.log(jelszo);
    
        // Submit the form
        document.getElementById('loginForm').submit();
        
    

   
    
}

function atvitel(){
    const url = "https://localhost:7090/api/Course/allcourses";
    fetch(url)
  .then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  })
  .then(data => {
    console.log(data); // Log the content of the JSON file to the console
    const dataDisplay = document.getElementById("dataDisplay");
    dataDisplay.innerHTML = '';
    data.forEach(item => {
         
      const nameElement = document.createElement("p");
      nameElement.textContent = "name: " + item.name;
    
      const ageElement = document.createElement("p");
      ageElement.textContent = "code: " + item.code;
    
      const departmentElement = document.createElement("p");
      departmentElement.textContent = "department: " + item.department;

      const creditElement = document.createElement("p");
      creditElement.textContent = "credit: " + item.credit;
    
      dataDisplay.appendChild(nameElement);
      dataDisplay.appendChild(ageElement);
      dataDisplay.appendChild(departmentElement);
      dataDisplay.appendChild(creditElement);

    });
  })
  .catch(error => {
    console.error('There was a problem with the fetch operation:', error);
  });
}

function sajatlista(){
  const url = "https://localhost:7090/api/Course/courseid";
  fetch(url)
  .then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  })
  .then(data => {
    console.log(data); // Log the content of the JSON file to the console
    const dataDisplay = document.getElementById("dataDisplay");
    dataDisplay.innerHTML = '';
    data.forEach(item => {
         
      const nameElement = document.createElement("p");
      nameElement.textContent = "name: " + item.name;
    
      const ageElement = document.createElement("p");
      ageElement.textContent = "code: " + item.code;
    
      const departmentElement = document.createElement("p");
      departmentElement.textContent = "department: " + item.department;

      const creditElement = document.createElement("p");
      creditElement.textContent = "credit: " + item.credit;
    
      dataDisplay.appendChild(nameElement);
      dataDisplay.appendChild(ageElement);
      dataDisplay.appendChild(departmentElement);
      dataDisplay.appendChild(creditElement);

    });
  })
  .catch(error => {
    console.error('There was a problem with the fetch operation:', error);
  });

}
/*
function tanszszures(){
    var tanszekmezo = document.getElementById('tansz');
    var tanszekszur = tanszekmezo.value;
    console.log(tanszekszur);

    const url = "https://localhost:7090/api/Course/allcourses";
    fetch(url)
  .then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  })
  .then(data => {
    console.log(data); // Log the content of the JSON file to the console
    const dataDisplay = document.getElementById("dataDisplay");
    dataDisplay.innerHTML = '';
    data.forEach(item => {
        var hasonlit=item.department;
        if(hasonlit.includes(tanszekszur)){
            const nameElement = document.createElement("p");
            nameElement.textContent = "name: " + item.name;
          
            const ageElement = document.createElement("p");
            ageElement.textContent = "code: " + item.code;
          
            const departmentElement = document.createElement("p");
            departmentElement.textContent = "department: " + item.department;
      
            const creditElement = document.createElement("p");
            creditElement.textContent = "credit: " + item.credit;
          
            dataDisplay.appendChild(nameElement);
            dataDisplay.appendChild(ageElement);
            dataDisplay.appendChild(departmentElement);
            dataDisplay.appendChild(creditElement);
        }
    });
  })
  .catch(error => {
    console.error('There was a problem with the fetch operation:', error);
  });
}
*/





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