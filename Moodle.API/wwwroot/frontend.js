
function bejelentkezes(){
const url = "http://localhost:7090/api/Test";

fetch(url)
    .then(response => {
        // Check if the request was successful (status code 200)
        if (response.ok) {
            // Parse the response as JSON (or text, if needed)
            return response.json();
        } else {
            throw new Error(`Error: Status code ${response.status}`);
        }
    })
    .then(data => {
        // Handle the retrieved data
        console.log("Response data:", data);
    })
    .catch(error => {
        console.error("Error:", error.message);
    });
}