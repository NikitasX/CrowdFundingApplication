﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$.ajax({
    url: "/Project/ListProjects",
    type: "GET"
}).done((projects) => {
    projects.forEach(project => {

        if (project.ProjectMedia.length == 0) {
            var projectImage = '/images/image-not-found.png';
        } else {
            var projectImage = project.ProjectMedia[0].MediaURL;
        }
        let card = `
             <div class="col col-sm-3 card">
                <img class="card-img-top" src="${projectImage}" alt="Project Image">
                <div class="card-body">
                    <h5 class="card-title">${project.ProjectTitle}</h5>
                    <p class="card-text">${project.ProjectDescription}</p>
                    <a href="#" class="btn btn-primary">Go somewhere</a>
                </div>
            </div>`;

        $('.js-view-project-list').append(card);
    });

}).fail((xhr) => {
});

$.ajax({

    url: '/User/ListUsers',
    type: 'GET',
    data: {
        UserEmail: email,
        Uservat: vatnumber,
        UserFirstName: firstname,
        UserLastName: lastname,
        UserPhone: phone
    }
}).done((users) => {

    let $userList = $('.js-user-list');
    $userList.html('');
    users.forEach(element => {
        let listItem =
            `<tr>
                    <td>${element.firstname}</td>
                    <td>${element.email}</td>
                    <td>${element.firstname}</td>
                    <td>${element.lastname}</td>
                    <td>${element.phone}</td>
                </tr>`;
        $userList.append(listItem);
    });

}).fail((xhr) => {
   
});



