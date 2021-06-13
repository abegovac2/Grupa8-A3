"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();

function chatNotification(postsId, notificationId, message) {
    var notif = "chatNotif";
    notif = notif.concat(parseInt(postsId));
    var chat = document.getElementById(notif);
    if(chat != null) chat.className = "my_notification";

    var notificationList = document.getElementById("notificationList");
    if (notificationList == null) return;

    var html = '<a class="my-notification-single" href=/Chats/Details/'+ postsId +'>';
    html += '<img width="50" height="50" src="/images/chat-svgrepo-com.svg" />';
    html += '<p>' + message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;") + '</p>';
    html += '<img width = "30" height = "30" src = "/images/notification.svg" />';
    html += '</a>'

    notificationList.innerHTML += html;
}

function questionNotification( postsId, notificationId, message) {
    var notificationList = document.getElementById("notificationList");
    if (notificationList == null) return;

    var html = '<a class="my-notification-single" href=/Question/Details/' + parseInt(postsId) + '>';
    html += '<img width="50" height="50" src="/images/question-mark.svg" />';
    html += '<p>' + message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;") + '</p>';
    html += '<img width = "30" height = "30" src = "/images/notification.svg" />';
    html += '</a>'

    notificationList.innerHTML += html;
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