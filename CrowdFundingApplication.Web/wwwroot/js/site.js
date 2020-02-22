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
                    <label class="col col-sm-12 text-center">${progress}%
                        <div class="progress mb-3">
                          <div class="progress-bar progress-bar-striped" role="progressbar"  
                                aria-valuenow="${progress}" aria-valuemin="0" style="width:${progress}%;" aria-valuemax="100">
                          </div>
                        </div>
                    </label>
                    <a href="#" class="btn btn-primary">ViewProject</a>
                </div>
            </div>`;

        $('.js-view-project-list').append(card);
    });
});