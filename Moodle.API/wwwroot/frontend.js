
function bejelentkezes(){
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
  })
  .catch(error => {
    console.error('There was a problem with the fetch operation:', error);
  });
      
}

document.addEventListener("DOMContentLoaded", function () {
  const url = "https://localhost:7090/api/Test/probajson";
  fetch(url)
      .then(response => response.json())
      .then(data => {
        console.log(data);  
        const dataDisplay = document.getElementById("dataDisplay");
          
         /* // Create HTML elements to display the JSON data
          const nameElement = document.createElement("p");
          nameElement.textContent = "Name: " + data.name;

          const ageElement = document.createElement("p");
          ageElement.textContent = "Age: " + data.age;

          const cityElement = document.createElement("p");
          cityElement.textContent = "City: " + data.city;

          // Append the elements to the "dataDisplay" div
          dataDisplay.appendChild(nameElement);
          dataDisplay.appendChild(ageElement);
          dataDisplay.appendChild(cityElement);*/
      })
      .catch(error => console.error("Error fetching JSON data:", error));
});


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
