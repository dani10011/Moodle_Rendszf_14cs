async function obtainNewAccessToken() {
    try {
        const refreshToken = sessionStorage.getItem('refreshToken');

        // Send refresh token to dedicated refresh token endpoint
        const response = await fetch('https://localhost:7090/api/Authentication/refreshtoken', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ refreshToken })
        });

        if (response.ok) {
            const data = await response.json();
            return data.accessToken; // Access token returned from refresh token endpoint
        } else {
            throw new Error('Failed to obtain new access token');
        }
    } catch (error) {
        console.error('Error obtaining new access token:', error.message);
        // Handle refresh token errors (e.g., invalid token, expired refresh token)
    }
}

//kurzus felvétele (csak diák tudja)
function kurzusFelvetel() {

    const retrievedData = sessionStorage.getItem('currentUserId');
    const url = "https://localhost:7090/api/Course/notincourseid?id=" + retrievedData;

    const token = sessionStorage.getItem('accessToken');
    const refreshtoken = sessionStorage.getItem('refreshtoken');
    const options = {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Refresh': 'Refresh ' + refreshtoken,
            'Content-Type': 'application/json'
        }
    };

    fetch(url, options)
        .then(response => {
            if (!response.ok) {
                if (response.headers.has('Token-Expired')) {
                    // Token expired, handle logout
                    console.error('Token expired, please log in again.');

                    sessionStorage.removeItem('accessToken');
                    sessionStorage.removeItem('currentUserId');

                    window.location.href = 'frontend.html';
                } else if (!response.headers.has('accessToken')) {
                    window.location.href = 'frontend.html';
                } else {
                    throw new Error('Network response was not ok');
                }
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


//Új kurzus felvétele (csak diák)
async function felvetel(courseId) {
    const retrievedData = sessionStorage.getItem('currentUserId');

    const NewCourse = {
        course_id : courseId,
        user_id : retrievedData,
        
    };

    try {
        const response = await fetch('https://localhost:7090/api/Course/NewCourse', {
            method: 'POST',
            body: JSON.stringify(NewCourse),
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

}