
const socket = new WebSocket('ws://localhost:8000');
var allapot = " "
var nev = getCurrentUserName() + " (" + allapot +")";
socket.onopen = function (event) {
    console.log('WebSocket connection opened.');
    const newMessage = document.createElement('p');
    newMessage.textContent = "Sikeresen csatlakozott a chat-hez!";
    output.appendChild(newMessage);
    output.scrollTop = output.scrollHeight;
};

socket.onmessage = function (event) {
    const output = document.getElementById('output');
    const newMessage = document.createElement('p');
    newMessage.textContent = event.data;
    output.appendChild(newMessage);
    output.scrollTop = output.scrollHeight;
};

socket.onerror = function (error) {
    console.error('WebSocket error:', error);
    const newMessage = document.createElement('p');
    newMessage.textContent = "ERROR";
    output.appendChild(newMessage);
    output.scrollTop = output.scrollHeight;
};

socket.onclose = function (event) {
    console.log('WebSocket connection closed:', event);
    const newMessage = document.createElement('p');
    newMessage.textContent = "Lecsatlakozott a chat szerverről!";
    output.appendChild(newMessage);
    output.scrollTop = output.scrollHeight;
    socket.removeEventListener('message', onMessage);
    socket.removeEventListener('error', onError);
};

function sendMessage(event) {
    event.preventDefault();
    const messageInput = document.getElementById('messageInput');
    const username = nev;
    const message = messageInput.value;
    if (username && message) {
        socket.send(username + ': ' + message);
        messageInput.value = '';
    } else {
        alert('Üres üzenetet nem küldhet!');
    }
}
