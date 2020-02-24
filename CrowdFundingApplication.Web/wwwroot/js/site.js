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

}).fail((xhr) => {
});


$('.js-submit-project').on('click', () => {
    $('.js-submit-project').attr('disable', true);
    let projecttitle = $('.js-projecttitle').val();
    let projectdescription = $('.js-projectdescription').val();
    let projectgoal = parseFloat($('.js-projectgoal').val());
    //  let projectcategory = $('.js-projectcategory').val();
    $.ajax({
        url: 'https://localhost:5001/project/AddProject',
        type: 'POST',
        contentType: 'application/json',
        data: {
            projecttitle: projecttitle,
            projectdescription:projectdescription,
            projectgoal: projectgoal
            //  projectCategory=projectcategory
        }
    }).done((project) => {
        debugger;
        //$('.alert').hide();
        //let $alertArea = $('.js-add-project-success');
        //$alertArea.html(`Successfully added project with id ${project.id}`);
        //$alertArea.show();
        //$('form.js-add-project').hide();
    }).fail((xhr) => {
        debugger;
        //$('.alert').hide();

        //let $alertArea = $('.js-add-project-alert');
        //$alertArea.html(xhr.responseText);
        //$alertArea.fadeIn();


    });
});



var currentTab = 0; // Current tab is set to be the first tab (0)
showTab(currentTab); // Display the current tab

function showTab(n) {
    // This function will display the specified tab of the form ...
    var x = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    // ... and fix the Previous/Next buttons:
    if (n == 0) {
        document.getElementById("prevBtn").style.display = "none";
    } else {
        document.getElementById("prevBtn").style.display = "inline";
    }
    if (n == (x.length - 1)) {
        document.getElementById("nextBtn").innerHTML = "Submit";
    } else {
        document.getElementById("nextBtn").innerHTML = "Next";
    }
    // ... and run a function that displays the correct step indicator:
    fixStepIndicator(n)
}

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
        // If a field is empty...
        if (y[i].value == "") {
            // add an "invalid" class to the field:
            y[i].className += " invalid";
            // and set the current valid status to false:
            valid = false;
        }
    }
    // If the valid status is true, mark the step as finished and valid:
    if (valid) {
        document.getElementsByClassName("step")[currentTab].className += " finish";
    }
    return valid; // return the valid status
}

function fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class to the current step:
    x[n].className += " active";
}
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
    });
});

$('.js-add-post').on('click', () => {

    let postFormData = $('.postForm').serializeArray();

    $('.js-add-post').prop('disabled', true);

    let data = JSON.stringify({
        PostTitle : postFormData[0].value,
        PostExcerpt : postFormData[1].value
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
})

$('.js-incentive').on('click', function() {
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
