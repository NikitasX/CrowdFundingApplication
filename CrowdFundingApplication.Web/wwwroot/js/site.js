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