"use strict";

$showTriggerInfo = document.getElementById("showTriggerInfo");
$showJobInfo = document.getElementById("showJobInfo");
var entries = [];
var numberOfEntriesToShow = 50;

function showMessage(message) {
    while (entries.length >= numberOfEntriesToShow) {
        entries.shift();
    }

    const value = {
        date: moment(),
        message: message
    };
    entries.push(value);
    console.log(entries);
}

var connection = new signalR.HubConnectionBuilder().withUrl("/api/liveLogHub").build();

console.log(connection);

connection
    .on("triggerFired", (trigger) => {
        if ($showTriggerInfo.val())
            showMessage(`Trigger <strong>${trigger.Name}.${trigger.Group}</strong> fired`);
    })
    .on("triggerMisfired", (trigger) => {
        if ($showTriggerInfo.val())
            showMessage(`<strong>Trigger ${trigger.Name}.${trigger.Group} misfired</strong>`);
    })
    .on("triggerCompleted", (trigger) => {
        if ($showTriggerInfo.val())
            showMessage(`Trigger <strong>${trigger.Name}.${trigger.Group}</strong> has completed`);
    })
    .on("triggerPaused", (triggerKey) => {
        if ($showTriggerInfo.val())
            showMessage(`Trigger <strong>${triggerKey.Name}.${triggerKey.Group}</strong> was paused`);
    })
    .on("triggerResumed", (triggerKey) => {
        if ($showTriggerInfo.val())
            showMessage(`Trigger <strong>${triggerKey.Name}.${triggerKey.Group}</strong> was resumed`);
    })
    .on("jobPaused", (jobKey) => {
        if ($showJobInfo)
            showMessage(`Job <strong>${jobKey.Name}.${jobKey.Group}</strong> was paused`);
    })
    .on("jobResumed", (jobKey) => {
        if ($showJobInfo)
            showMessage(`Job <strong>${jobKey.Name}.${jobKey.Group}</strong> was resumed`);
    })
    .on("jobToBeExecuted", (jobKey, triggerKey) => {
        if ($showJobInfo)
            showMessage(`Starting to execute job <strong>${jobKey.Name}.${jobKey.Group}</strong> triggered by trigger <strong>${triggerKey.Name}.${triggerKey.Group}</strong>...`);
    })
    .on("jobWasExecuted", (jobKey, triggerKey, errorMessage) => {
        if ($showJobInfo) {
            let message = `Job <strong>${jobKey.Name}.${jobKey.Group}</strong> was executed`;
            if (errorMessage) {
                message += " and ended with error: " + errorMessage;
            }
            showMessage(message);
        }
    });

connection.error(error => {
    showMessage(error);
});

connection.start().then(function () {
    showMessage("Connected");
}).catch(function (err) {
    showMessage("SignalR error: Could not start hub connection");
});