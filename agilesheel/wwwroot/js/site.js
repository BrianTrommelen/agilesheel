document.addEventListener('DOMContentLoaded', (event) => {
    if (boolURLContains('Tickets/Create')) {
        prefilTicketName();
        createRandomCode(8, 'Ticket_Code');
    }
});


function boolURLContains(f_URLString) {
    return window.location.href.indexOf(f_URLString) > -1;
}

function boolURLIs(f_URLString) {
    return window.location.pathname === f_URLString;
}

function createRandomCode(length, output) {
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let result = '';
    output = document.getElementById(output);
    const charactersLength = characters.length;
    for (let i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    output.value = result
}

function prefilTicketName() {
    const normal = document.getElementById('AmountNormalPrice');
    const child = document.getElementById('AmountChildrenPrice');
    const student = document.getElementById('AmountStudentsPrice');
    const older = document.getElementById('Amount65Price');
    const name = document.getElementById('Ticket_Name');
    normal.onclick = function () {
        if (normal.checked) { name.value = "Normal" }
    }
    child.onclick = function () {
        if (child.checked) { name.value = "Child" }
    }
    student.onclick = function () {
        if (student.checked) { name.value = "Student" }
    }
    older.onclick = function () {
        if (older.checked) { name.value = "Older person" }
    }
}