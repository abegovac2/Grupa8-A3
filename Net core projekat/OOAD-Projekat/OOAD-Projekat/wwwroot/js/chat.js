"use strict";



var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendMessageButton").disabled = true;


var createReceiveMessage = function (user, message, time) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var incoming = document.createElement("div");
    incoming.className = "incoming_msg";

    var incoming_img = document.createElement("div");
    incoming_img.className = "incoming_msg_img";
    var img = document.createElement("img");

    img.src = "/images/profile.svg";
    img.alt = "sunil";

    incoming_img.appendChild(img);

    var recived_msg = document.createElement("div");
    recived_msg.className = "received_msg";

    var recived_msg1 = document.createElement("div");
    recived_msg1.className = "received_withd_msg";

    var p = document.createElement("p");
    p.textContent = msg;

    var span = document.createElement("span");
    span.className = "time_date";
    span.textContent = time;

    var span1 = document.createElement("span");
    span1.className = "time_date";
    span1.textContent = user;

    recived_msg1.appendChild(span1);
    recived_msg1.appendChild(p);
    recived_msg1.appendChild(span);
    recived_msg.appendChild(recived_msg1);

    incoming.appendChild(incoming_img);
    incoming.appendChild(recived_msg);

    return incoming;
}

var createSendersMessage = function (user, message, time) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var outgoing = document.createElement("div");
    outgoing.className = "outgoing_msg"
    var sent_msg = document.createElement("div");
    sent_msg.className = "sent_msg";

    var p = document.createElement("p");
    p.textContent = msg;

    var span = document.createElement("span");
    span.className = "time_date";
    span.textContent = time;

    sent_msg.appendChild(p);
    sent_msg.appendChild(span);

    outgoing.appendChild(sent_msg);

    return outgoing;
}

connection.on("ReceiveMessage", function (user, connectionId, message, time) {
    var incoming = (connection.connectionId == connectionId) ? createSendersMessage(user, message, time) : createReceiveMessage(user, message, time);

    document.getElementById("messagesList").appendChild(incoming);

    var objDiv = document.getElementById("messagesList");
    objDiv.scrollTop = objDiv.scrollHeight;
});

connection.start().then(function () {
    document.getElementById("sendMessageButton").disabled = false;
    var chatId = document.getElementById("chatInput").value;
    connection.invoke("JoinGroup", chatId);
}).catch(function (err) {
    return console.error(err.toString());
});

/*
var saveMessage = function () {
    $.post({
        url: '@Url.Action("SendMessage", "Chats")',
        data: {
            chatId: $("#chatInput").val(),
            name: $("#userInput").val(),
            text: $("#messageInput").val()
        },
        success: (err) => { console.log("esi mi dobarrrrrrrrrrrrr") },
        error: null
    })
    onSubmit()
}*/


var sendMessageSR = function () {
    var userName = document.getElementById("userInput").value;
    var chatId = document.getElementById("chatInput").value;
    var message = document.getElementById("messageInput");

    //Emptys the message bar
    if (message.value == "") return;
    var text = message.value;
    message.value = "";
    //invokes ChatHub methods
    connection.invoke("SendMessage", chatId, userName, text).catch(function (err) {
        return console.error(err.toString());
    });
}