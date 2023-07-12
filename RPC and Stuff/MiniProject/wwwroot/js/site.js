const uri = 'api/employees';
let employees = [];

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addPositionTextbox = document.getElementById('add-position');
    const addSalaryTextbox = document.getElementById('add-salary');
    const addDateHiredTextbox = document.getElementById('add-dateHired');

    const item = {
        name: addNameTextbox.value.trim(),
        position: addPositionTextbox.value.trim(),
        salary: addSalaryTextbox.value.trim(),
        dateHired: addDateHiredTextbox.value.trim()
    };

    if (!onlyLettersAndSpaces(item.name)) {
        console.error('Name cannot contain numbers.');
        showNotification('Name cannot contain numbers.');
        return;
    } else if (item.position.length > 20 || item.position.length < 0) {
        console.error('Position is too long.');
        showNotification('Position is too long.');
        return;
    } else if (item.salary < 0) {
        console.error('Salary cannot be negative.');
        showNotification('Salary cannot be negative.');
        return;
    } else if (new Date(item.dateHired) > new Date()) {
        console.error('Hired date is invalid.');
        showNotification('Hired date is invalid.');
        return;
    }

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            showNotification('Employee was added successfully');
            getItems();
            addNameTextbox.value = '';
            addPositionTextbox.value = '';
            addSalaryTextbox.value = '';
            addDateHiredTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function showEmployeeById() {
    const employeeIdInput = document.getElementById('employee-id');
    const employeeId = employeeIdInput.value.trim();

    if (employeeId !== '') {
        getEmployeeById(employeeId);
    } else {
        console.error('Please enter a valid employee ID.');
        showNotification('Please enter a valid employee ID.');
    }
}

function getEmployeeById(id) {
    fetch(`${uri}/${id}`)
        .then(response => response.json())
        .then(data => _displayItem(data, "specific-id-employees"))
        .catch(error => console.error(`Unable to get employee with ID ${id}.`, error));
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        name: document.getElementById('edit-name').value.trim(),
        position: document.getElementById('edit-position').value.trim(),
        salary: document.getElementById('edit-salary').value.trim(),
        dateHired: document.getElementById('edit-dateHired').value.trim()
    };

    if (!onlyLettersAndSpaces(item.name)) {
        console.error('Name cannot contain numbers.');
        showNotification('Name cannot contain numbers.');
        return;
    } else if (item.position.length > 20 || item.position.length < 0) {
        console.error('Position is too long.');
        showNotification('Position is too long.');
        return;
    } else if (item.salary < 0) {
        console.error('Salary cannot be negative.');
        showNotification('Salary cannot be negative.');
        return;
    } else if (new Date(item.dateHired) > new Date()) {
        console.error('Hired date is invalid.');
        showNotification('Hired date is invalid.');
        return;
    }

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => {
            refreshPage('Update');
            showNotification('Employee was successfully updated');
        })
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => refreshPage('Delete'))
        .catch(error => console.error('Unable to delete item.', error));
}

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data, "all-employees"))
        .catch(error => console.error('Unable to get items.', error));
}

function filterByPosition() {
    const filterPositionTextbox = document.getElementById('filter-position');
    const position = filterPositionTextbox.value.trim();

    if (position === "") {
        fetch(`${uri}`)
            .then(response => response.json())
            .then(data => _displayItems(data, "filtered-employees"))
            .catch(error => console.error('Unable to filter employees.', error));
    } else {
        fetch(`${uri}/filter/${position}`)
            .then(response => response.json())
            .then(data => _displayItems(data, "filtered-employees"))
            .catch(error => console.error('Unable to filter employees.', error));
    }
}

function fetchNoEmployees() {
    fetch(`${uri}/noemployees`)
        .then(response => response.json())
        .then(data => _displayNoEmployees(data))
        .catch(error => console.error('Unable to get the number of employees.', error));
}

function getAuthors() {
    fetch(`${uri}/authors`)
        .then(response => response.text())
        .then(data => _displayAuthors(data))
        .catch(error => console.error('Unable to get authors.', error));
}

function displayEditForm(id) {
    const item = employees.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-position').value = item.position;
    document.getElementById('edit-salary').value = item.salary;
    const dateHired = item.dateHired.split('T')[0];
    document.getElementById('edit-dateHired').value = dateHired;
    document.getElementById('editForm').style.display = 'block';
}

function _displayItem(data, element) {
    if (data.id === undefined) {
        showNotification('Please enter a valid employee ID.');
    }
    else {
        showNotification('');
        _displayItems([data], element);
    }
}

function _displayItems(data, element) {
    const tBody = document.getElementById(element);
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(item => {
        let tr = tBody.insertRow();

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let td0 = tr.insertCell(0);
        let idNode = document.createTextNode(item.id);
        td0.appendChild(idNode);

        let td1 = tr.insertCell(1);
        let nameNode = document.createTextNode(item.name);
        td1.appendChild(nameNode);

        let td2 = tr.insertCell(2);
        let posNode = document.createTextNode(item.position);
        td2.appendChild(posNode);

        let td3 = tr.insertCell(3);
        let salNode = document.createTextNode(item.salary);
        td3.appendChild(salNode);

        let td4 = tr.insertCell(4);
        let dateNode = document.createTextNode(item.dateHired.split('T')[0]);
        td4.appendChild(dateNode);

        let td5 = tr.insertCell(5);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(6);
        td6.appendChild(deleteButton);
    });

    employees = data;
}

function _displayNoEmployees(data) {
    const countElement = document.getElementById('employee-count');
    if (data == 0) {
        countElement.innerText = `There are no employees.`;
    }
    else {
        countElement.innerText = 'Number of employees: ' + data;
    }
}

function _displayAuthors(authors) {
    const authorsElement = document.getElementById('authorstext');
    authorsElement.innerText = authors;
}

function showNotification(message) {
    const notificationElement = document.getElementById('notification');
    notificationElement.innerText = message;
}

function toggleView(view) {
    const formContainer = document.getElementById('form-container');
    const specificIdContainer = document.getElementById('specific-id-container');
    const allContainer = document.getElementById('all-container');
    const filterContainer = document.getElementById('filter-container');
    const countContainer = document.getElementById('count-container');
    const authorsContainer = document.getElementById('authors-container');

    formContainer.style.display = 'none';
    specificIdContainer.style.display = 'none';
    allContainer.style.display = 'none';
    filterContainer.style.display = 'none';
    countContainer.style.display = 'none';
    authorsContainer.style.display = 'none';
    closeInput();
    showNotification('');

    if (view === 'form') {
        formContainer.style.display = 'block';
    } else if (view === 'specific-id') {
        specificIdContainer.style.display = 'block';
    } else if (view === 'all') {
        allContainer.style.display = 'block';
        getItems();
    } else if (view === 'filter') {
        filterContainer.style.display = 'block';
    } else if (view === 'count') {
        countContainer.style.display = 'block';
        fetchNoEmployees();
    } else if (view === 'authors') {
        authorsContainer.style.display = 'block';
        getAuthors();
    }
}

function refreshPage(method) {
    const specificIdContainer = document.getElementById('specific-id-container');
    const allContainer = document.getElementById('all-container');
    const filterContainer = document.getElementById('filter-container');

    if (specificIdContainer.style.display === 'block') {
        if (method == 'Update') {
            showEmployeeById();
        } else if (method == 'Delete') {
            location.reload();
        }
    } else if (allContainer.style.display === 'block') {
        getItems();
    } else if (filterContainer.style.display === 'block') {
        filterByPosition();
    }
}

function onlyLettersAndSpaces(str) {
    return /^[A-Za-z\s]*$/.test(str);
}
