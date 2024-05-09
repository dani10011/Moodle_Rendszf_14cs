//bejelentkezési funkció
async function bejelentkezes() { //aszinkron: várhat egy művelet befejezésére
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
  
    const loginData = {
      Username: username,
      Password: password
    };
  
    const response = await fetch('https://localhost:7090/api/Authentication/login', { //elküldi a felhasználónév jelszó párost, await megállítja a funkciót amíg válaszra vár
      method: 'POST',
      body: JSON.stringify(loginData),
      headers: { 'Content-Type': 'application/json' }
    });
  
    if (!response.ok) { //ellenőrzi a login válaszát, ha Ok, átvisz a mainPage-re
      const message = await response.text();
      alert(message);
    }
    else {
      const data = await response.json();
      const message = data.message;
      const userId = data.userId;
      const userName = data.name;
      const role = data.role;
      const token = data.token;
  
        sessionStorage.setItem('currentUserId', userId);
        sessionStorage.setItem('currentUserName', userName);
        sessionStorage.setItem('currentUserRole', role);
        sessionStorage.setItem('accessToken', token);
        console.log("Access Token: ", token);
  
      currentUserId = userId;
      currentUserName = userName;
      currentUserRole = role;
      console.log("Id: ", currentUserId);
      console.log("Id: ", currentUserName);
      console.log("Login successful:", message);
  
      uzenetMegjelenites(message);
      //alert(message);
  
      
      setTimeout(function () {
        if (role == 'tanár') {
          window.location.href = 'mainPage_teacher.html';
        }
        else if (role == 'diák') {
          window.location.href = 'mainPage_student.html';
        }
      }, 2000);
    }
  
  }
  
  
  
  //megjeleníti/időzíti a felugró üzenetet
  function uzenetMegjelenites(message) {
    const popup = document.createElement('div');
    popup.textContent = message;
    popup.style.position = 'fixed';
    popup.style.top = '50%';
    popup.style.left = '50%';
    popup.style.transform = 'translate(-50%, -50%)';
    popup.style.backgroundColor = '#90EE90';
    popup.style.padding = '40px';
    popup.style.borderRadius = '20px';
    popup.style.boxShadow = '0 0 20px rgba(0, 0, 0, 0.2)';
    popup.style.zIndex = '9999';
    popup.style.textAlign = 'center';
    popup.style.maxWidth = '500px';
    popup.style.fontSize = '24px';
    popup.style.fontWeight = 'bold';
    popup.style.color = '#333';
    popup.style.lineHeight = '1.5';
  
    // Append the popup element to the body
    document.body.appendChild(popup);
  
    // Automatically close the popup after the specified duration
    setTimeout(function () {
      popup.remove();
    }, 2000);
  }
  