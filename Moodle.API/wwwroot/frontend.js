
function bejelentkezes(){

    
    const url = "http://localhost:7090/api/Test";

fetch(url)
    .then(response => {
        
        if (response.ok) {
        
            return response.json();
        } else {
            throw new Error(`Error: Status code ${response.status}`);
        }
    })
    .then(data => {
       
        console.log("Response data:", data);
    })
    .catch(error => {
        console.error("Error:", error.message);
    });
}