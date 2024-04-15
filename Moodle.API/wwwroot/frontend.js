
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





async function bejelentkezes() {
  var username = document.getElementById("username").value;
  var password = document.getElementById("password").value;

  const loginData = {
      Username: username,
      Password: password
  };

  const response = await fetch('https://localhost:7090/api/Authentication/login', {
      method: 'POST',
      body: JSON.stringify(loginData),
      headers: { 'Content-Type': 'application/json' }
  });

  if (!response.ok) {
      const message = await response.text();
      alert(message);
  } else {
      const message = await response.text();
      alert(message);
      window.location.href = 'mainPage.html'
  }
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

async function fetchDataTanszek() {
  const url = "https://localhost:7090/api/Course/allcourses";

  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    const data = await response.json();

    console.log(data);

    const departmentList = document.getElementById("departmentList");
    departmentList.innerHTML = '';

    //
    const uniqueDepartments = new Set();

    data.forEach(item => {
      uniqueDepartments.add(item.department);
    });


    for (const department of uniqueDepartments) {
      const option = document.createElement("option");
      option.text = department;
      departmentList.appendChild(option);
    }



    // Minden elem kiiratása
    data.forEach(item => {
      const nameElement = document.createElement("p");
      nameElement.textContent = "name: " + item.name;
      dataDisplay.appendChild(nameElement);

      const ageElement = document.createElement("p");
      ageElement.textContent = "code: " + item.code;
      dataDisplay.appendChild(ageElement);

      const departmentElement = document.createElement("p");
      departmentElement.textContent = "department: " + item.department;
      dataDisplay.appendChild(departmentElement);

      const creditElement = document.createElement("p");
      creditElement.textContent = "credit: " + item.credit;
      dataDisplay.appendChild(creditElement);
    });

    departmentList.addEventListener("change", function () {
      const selectedDepartment = this.value;
      const dataDisplay = document.getElementById("dataDisplay");
      dataDisplay.innerHTML = '';


      const filteredData = data.filter(item => item.department === selectedDepartment);

      // Szűrt elemek kilistázása
      filteredData.forEach(item => {
        const nameElement = document.createElement("p");
        nameElement.textContent = "name: " + item.name;
        dataDisplay.appendChild(nameElement);

        const ageElement = document.createElement("p");
        ageElement.textContent = "code: " + item.code;
        dataDisplay.appendChild(ageElement);

        const departmentElement = document.createElement("p");
        departmentElement.textContent = "department: " + item.department;
        dataDisplay.appendChild(departmentElement);

        const creditElement = document.createElement("p");
        creditElement.textContent = "credit: " + item.credit;
        dataDisplay.appendChild(creditElement);
      });
    });
  } catch (error) {
    console.error('There was a problem with the fetch operation:', error);
  }
}


async function fetchDataResztvevoListazas() {
  const url = "https://localhost:7090/api/Course/allcourses";

  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    const data = await response.json();

    console.log(data);

    const studentList = document.getElementById("studentList");
    studentList.innerHTML = '';  // Clear any previous options

    // Create options for each course
    data.forEach(course => {
      const option = document.createElement("option");
      option.text = course.name + ' (' + course.code + ')';
      option.value = course; // Store entire course object in value
      studentList.appendChild(option);
    });

    studentList.addEventListener("change", function () {
      const selectedCourse = this.value; // Get the entire course object
      const dataDisplay = document.getElementById("dataDisplay");
      dataDisplay.innerHTML = ''; // Clear previous content

      // Check if a course is selected
      if (selectedCourse) {
        const enrolledStudents = selectedCourse.enrolled_students;
        dataDisplay.textContent = "Enrolled Students:";

        // Display enrolled students (optional)
        enrolledStudents.forEach(student => {
          const studentElement = document.createElement("p");
          studentElement.textContent = student;
          dataDisplay.appendChild(studentElement);
        });
      } else {
        dataDisplay.textContent = "Please select a course.";
      }
    });
  } catch (error) {
    console.error('There was a problem with the fetch operation:', error);
  }
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