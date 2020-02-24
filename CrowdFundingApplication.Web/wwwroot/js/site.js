// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

        var progress = (parseInt(project.ProjectCapitalAcquired * 100)
            / parseInt(project.ProjectFinancialGoal));

        progress = progress.toFixed();

        let card = `
             <div class="col col-sm-3 card">
                <img class="card-img-top" src="${projectImage}" alt="Project Image">
                <div class="card-body">
                    <h5 class="card-title">${project.ProjectTitle}</h5>
                    <p class="card-text">${project.ProjectDescription}</p>
                    <label class="col col-sm-12 text-center font-weight-bold text-primary">${progress}% funded
                        <div class="progress mb-1">
                          <div class="progress-bar progress-bar-striped" role="progressbar"  
                                aria-valuenow="${progress}" aria-valuemin="0" style="width:${progress}%;" aria-valuemax="100">
                          </div>
                        </div>
                        ${project.ProjectCapitalAcquired} / ${project.ProjectFinancialGoal} ($)
                    </label>
                    <a href="/project/view/${project.ProjectId}" class="col col-sm-12 btn btn-primary">View Project</a>
                </div>
            </div>`;

        $('.js-view-project-list').append(card);

    });
});

$.ajax({
    url: "/Project/ListProjectsPopular",
    type: "GET"
}).done((projects) => {
    projects.forEach(project => {

        if (project.ProjectMedia.length == 0) {
            var projectImage = '/images/image-not-found.png';
        } else {
            var projectImage = project.ProjectMedia[0].MediaURL;
        }

        var progress = (parseInt(project.ProjectCapitalAcquired * 100)
            / parseInt(project.ProjectFinancialGoal));

        progress = progress.toFixed();

        let card = `
             <div class="col col-sm-3 card">
                <img class="card-img-top" src="${projectImage}" alt="Project Image">
                <div class="card-body">
                    <h5 class="card-title">${project.ProjectTitle}</h5>
                    <p class="card-text">${project.ProjectDescription}</p>
                    <label class="col col-sm-12 text-center font-weight-bold text-primary">${progress}% funded
                        <div class="progress mb-1">
                          <div class="progress-bar progress-bar-striped mb-0" role="progressbar"  
                                aria-valuenow="${progress}" aria-valuemin="0" style="width:${progress}%;" aria-valuemax="100">
                          </div>
                        </div>
                        ${project.ProjectCapitalAcquired} / ${project.ProjectFinancialGoal} ($)
                    </label>
                    <a href="/project/view/${project.ProjectId}" class="col col-sm-12 btn btn-primary">View Project</a>
                </div>
            </div>`;

        $('.js-view-project-list-popular').append(card);
    })
}).fail((xhr) => {
});

$.ajax({
    url: "/Project/ListProjectsPopular",
    type: "GET"
}).done((projects) => {
    projects.forEach(project => {

        if (project.ProjectMedia.length == 0) {
            var projectImage = '/images/image-not-found.png';
        } else {
            var projectImage = project.ProjectMedia[0].MediaURL;
        }

        var progress = (parseInt(project.ProjectCapitalAcquired * 100)
            / parseInt(project.ProjectFinancialGoal));

        progress = progress.toFixed();

        let card = `
             <div class="col col-sm-3 card">
                <img class="card-img-top" src="${projectImage}" alt="Project Image">
                <div class="card-body">
                    <h5 class="card-title">${project.ProjectTitle}</h5>
                    <p class="card-text">${project.ProjectDescription}</p>
                    <label class="col col-sm-12 text-center font-weight-bold text-primary">${progress}% funded
                        <div class="progress mb-1">
                          <div class="progress-bar progress-bar-striped mb-0" role="progressbar"  
                                aria-valuenow="${progress}" aria-valuemin="0" style="width:${progress}%;" aria-valuemax="100">
                          </div>
                        </div>
                        ${project.ProjectCapitalAcquired} / ${project.ProjectFinancialGoal} ($)
                    </label>
                    <a href="/project/view/${project.ProjectId}" class="col col-sm-12 btn btn-primary">View Project</a>
                </div>
            </div>`;

        $('.js-view-project-list-user').append(card);
    })
}).fail((xhr) => {
});

$('.js-incentive').on('click', function () {
    let projId = $('#projectId').val();
    let incentiveId = $(this).find('input[type="hidden"]').val();

    $.ajax({
        url: `https://localhost:5001/project/AddProjectBacker/${projId}/${incentiveId}`,
        type: 'POST'
    }).done((incentive) => {
        location.reload();
    }).fail((xhr) => {
        alert(xhr.responseText);
    });
});

$('.js-add-post').on('click', () => {

    let postFormData = $('.postForm').serializeArray();

    $('.js-add-post').prop('disabled', true);

    let data = JSON.stringify({
        PostTitle: postFormData[0].value,
        PostExcerpt: postFormData[1].value
    });

    $.ajax({
        url: `/project/addprojectpost/${postFormData[2].value}`,
        type: 'POST',
        contentType: 'application/json',
        data: data
    }).done((post) => {
        location.reload();
    }).fail((xhr) => {
        alert(xhr.responseText);

        setTimeout(() => {
            $('.js-add-post').prop('disabled', false);
        }, 1000);
    });
});

$('.js-submit-project').on('click', () => {
    
    $('.js-submit-project').attr('disable', true);

    let postFormData = $('form:visible').serializeArray();

    let data = JSON.stringify({
        ProjectTitle: postFormData[0].value,
        ProjectDescription: postFormData[1].value,
        ProjectFinancialGoal: parseFloat(postFormData[2].value),
        ProjectCategory: postFormData[3].value,
        ProjectDateExpiring: postFormData[4].value
    });

    $.ajax({
        url: '/project/AddProject',
        type: 'POST',
        contentType: 'application/json',
        data: data
    }).done((project) => {

        $('.js-project-id').val(project.projectId);

        $('form[data-form="0"]').parent().hide();
        $('form[data-form="1"]').parent().fadeIn(1000);
    }).fail((xhr) => {
        alert(xhr.responseText);

        setTimeout(() => {
            $('.js-submit-project').prop('disabled', false);
        }, 1000);
    });
});

$('.js-submit-media').on('click', function () {

    var clickedButton = $(this).attr('class');

    let postFormData = $('form:visible').serializeArray();
    let projectId = $('.js-project-id').val();

    let data = JSON.stringify({
        MediaType: postFormData[0].value,
        MediaURL: postFormData[1].value
    });

    $.ajax({
        url: `/project/AddProjectMedia/${projectId}`,
        type: 'POST',
        contentType: 'application/json',
        data: data
    }).done((media) => {

        $('input[name="MediaURL"]').val("");

        if (!clickedButton.includes('continue')) {
            $('form[data-form="1"]').parent().hide();
            $('form[data-form="2"]').parent().fadeIn(1000);
        }
    }).fail((xhr) => {
        alert(xhr.responseText);

        setTimeout(() => {
            $('.js-submit-media').prop('disabled', false);
        }, 1000);
    });
});

$('.js-submit-incentive').on('click', function () {

    var clickedButton = $(this).attr('class');

    let postFormData = $('form:visible').serializeArray();
    let projectId = $('.js-project-id').val();

    let data = JSON.stringify({
        IncentiveTitle: postFormData[0].value,
        IncentivePrice: parseFloat(postFormData[1].value),
        IncentiveReward: postFormData[2].value,
        IncentiveDescription: postFormData[3].value
    });

    $.ajax({
        url: `/project/AddProjectIncentive/${projectId}`,
        type: 'POST',
        contentType: 'application/json',
        data: data
    }).done((media) => {

        $('input[type="text"], input[type="number"], textarea').val("");

        if (!clickedButton.includes('continue')) {
            window.location.replace(`/project/view/${projectId}`);
        }
    }).fail((xhr) => {
        alert(xhr.responseText);

        setTimeout(() => {
            $('.js-submit-media').prop('disabled', false);
        }, 1000);
    });
});

var currentTab = 0; // Current tab is set to be the first tab (0)
showTab(currentTab); // Display the current tab

function showTab(n) {

    let checker = $('.tab');

    if (checker.length != 0) {
        var x = document.getElementsByClassName("tab");

        x[n].style.display = "block";
    }
}
/*
function nextPrev(n) {
    // This function will figure out which tab to display
    var x = document.getElementsByClassName("tab");
    // Exit the function if any field in the current tab is invalid:
    if (n == 1 && !validateForm()) return false;
    // Hide the current tab:
    x[currentTab].style.display = "none";
    // Increase or decrease the current tab by 1:
    currentTab = currentTab + n;
    // if you have reached the end of the form... :
    if (currentTab >= x.length) {
        //...the form gets submitted:
        document.getElementById("regForm").submit();
        return false;
    }
    // Otherwise, display the correct tab:
    showTab(currentTab);
}

function validateForm() {
    // This function deals with validation of the form fields
    var x, y, i, valid = true;
    x = document.getElementsByClassName("tab");
    y = x[currentTab].getElementsByTagName("input");
    // A loop that checks every input field in the current tab:
    for (i = 0; i < y.length; i++) {
        if (y[i].getAttribute("type") != "date") {
            // If a field is empty...
            if (y[i].value == "") {
                // add an "invalid" class to the field:
                y[i].className += " invalid";
                // and set the current valid status to false:
                valid = false;
            }
        }
    }
    // If the valid status is true, mark the step as finished and valid:
    if (valid) {
        document.getElementsByClassName("step")[currentTab].className += " finish";
    }
    return valid; // return the valid status
}
*/