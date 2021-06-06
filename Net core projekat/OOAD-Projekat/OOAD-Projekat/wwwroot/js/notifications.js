﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();

function chatNotification(postsId, notificationId, message) {
    var chat = document.getElementById("chatNotif" + postId);
    if(chat.textContent.length == 0) chat.textContent = "ImaNesto";
}

function questionNotification( postsId, notificationId, message) {

}

function anwserNotification(postsId, notificationId, message) {

}




connection.on("NotifyUser", (postsId, notificationId, message) => {
    if (notificationId == 0) {
        chatNotification(userId, postsId, notificationId, message);
    } else if (notificationId == 1) {
        questionNotification(userId, postsId, notificationId, message);
    } else if (notificationId == 2) {
        anwserNotification(userId, postsId, notificationId, message);
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
}).then(function () {
    var idUser = document.getElementById("userId").value;
    document.getElementById("userId").innerHTML = "UserId: " + userId;
    connection.invoke("GetConnectionId", idUser).then(function (connectionId) {
        document.getElementById("signalRConnectionId").innerHTML = connectionId;
    })
});