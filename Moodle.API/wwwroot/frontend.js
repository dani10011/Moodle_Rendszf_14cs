
function bejelentkezes(){
    window.location.href='mainPage.html'

}

function atvitel(){
    const url = "https://localhost:7090/api/Test/probajson";
    fetch(url)
        .then(response => {
            
            if (response.ok) {
                
                return response.json();
                
            } else {
                throw new Error(`Error: Status code ${response.status}`);
            }
        })
        .then(data => {
        
            document.getElementById("proba").innerHTML = data;
            console.log(data);
        })
        .catch(error => {
            console.error("Error:", error.message);
        });
      
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
