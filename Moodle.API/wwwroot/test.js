
function bejelentkezes(){
    var felhasznalomezo = document.getElementById('felhasznalonev');
    var felhasznalonev = felhasznalomezo.value;
    var jelszomezo = document.getElementById('jelszo');
    var jelszo = jelszomezo.value;
    console.log(felhasznalonev);
    console.log(jelszo);

    window.location.href='mainPage.html'

}

function atvitel(){
    const url = "https://localhost:7090/api/Test/probajson";
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
    data.forEach(item => {
         
      const nameElement = document.createElement("p");
      nameElement.textContent = "name: " + item.name;
    
      const ageElement = document.createElement("p");
      ageElement.textContent = "code: " + item.code;
    
      const departmentElement = document.createElement("p");
      departmentElement.textContent = "credit: " + item.credit;

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


function tanszszures(){
    var tanszekmezo = document.getElementById('tansz');
    var tanszekszur = tanszekmezo.value;
    console.log(tanszekszur);
}

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
