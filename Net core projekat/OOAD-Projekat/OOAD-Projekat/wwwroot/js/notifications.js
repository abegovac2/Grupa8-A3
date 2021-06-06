"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();

function chatNotification(postsId, notificationId, message) {
    var notif = "chatNotif";
    notif = notif.concat(parseInt(postsId));
    var chat = document.getElementById(notif);
    chat.textContent = "True";
}

function questionNotification( postsId, notificationId, message) {

}

function anwserNotification(postsId, notificationId, message) {

}




connection.on("NotifyUser", (postsId, notificationId, message) => {
    if (notificationId == 2) {
        chatNotification(postsId, notificationId, message);
    }else if (notificationId == 1) {
        anwserNotification(postsId, notificationId, message);
    }else if (notificationId == 0) {
        questionNotification(postsId, notificationId, message);
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