document.addEventListener('DOMContentLoaded', function() {
    const addProjectForm = document.querySelector("#projectForm");
    const addProjectButton = document.querySelector("#addProjectBtn");
    const projectFormCloseBtn = document.querySelector("#projectFormClose");
    const projectMenuBtns = document.querySelectorAll(".project-menu-btn");
    const editMenus = document.querySelectorAll(".edit-menu");
    const editClose = document.querySelector("#editProjectFormClose");
    const editForm = document.querySelector("#editProjectForm");
    const editBtn = document.querySelector("#editProjectBtn");
    
    if(addProjectButton) {
        addProjectButton.addEventListener("click", ()=>{
            addProjectForm.classList.remove("hidden");
        })
    }

    if(addProjectButton){
        projectFormCloseBtn.addEventListener("click", ()=>{
                window.location.href = '/Dashboard/Dashboard';
        })
    }

    if(projectMenuBtns) {
        projectMenuBtns.forEach(btn => {
            btn.addEventListener("click", (e) => {
                e.stopPropagation();
    
                const card = btn.closest(".dashboard-card");
                const menu = card.querySelector(".edit-menu");
                const isCurrentlyHidden = menu.classList.contains("hidden");
    
                editMenus.forEach(menu => {
                    menu.classList.add("hidden");
                });
    
                if (isCurrentlyHidden) {
                    menu.classList.remove("hidden");
                }
    
            });
        });
        
    }

        if(editBtn) {
            
            editBtn.addEventListener("click", ()=>{
                editForm.classList.remove("hidden")
            })
        }

        if (editClose){
            editClose.addEventListener("click", ()=>{
                window.location.href = '/Dashboard/Dashboard';
            });
        }

    const listFilters = document.querySelectorAll(".list-filter");
    const dashboardCards = document.querySelectorAll(".dashboard-card");   
       
    if(listFilters){
        
           listFilters.forEach(filter =>{
               filter.addEventListener("click", ()=>{

                   listFilters.forEach(item =>{
                       item.classList.remove("filter-active")
                   });
                   filter.classList.add("filter-active");

                   const filterType = filter.dataset.filter.toLowerCase().trim();

                   dashboardCards.forEach(card => {
                       const isCompleted = card.dataset.status.toLocaleLowerCase().trim() === "true";
                       console.log(isCompleted);
                       console.log(filterType);

                       if (filterType === "completed" && !isCompleted){
                           card.classList.add("hidden")
                       } else if (filterType === "started" && isCompleted) {
                           card.classList.add("hidden")
                       } else {
                           card.classList.remove("hidden")
                       }
                   })
               })
           }) 
       }
       
    
    
    /*Cookie consent*/

    const consentBtn = document.querySelector(".accept-cookie");
    const banner = document.querySelector(".cookie-box");
    const declineBtn = document.querySelector(".decline-cookie");
    const cookieReviewBtn= document.querySelector(".cookie-review");
    
    if (consentBtn) {
        consentBtn.addEventListener("click", () => {
            document.cookie = consentBtn.dataset.cookieString;
            if (banner) {
                banner.classList.add("hidden");
            }
            if (cookieReviewBtn) {
                cookieReviewBtn.classList.remove("hidden");
            }
        });
    }

    
    if (declineBtn) {
        declineBtn.addEventListener("click", () => {
            document.cookie = ".AspNet.Consent=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
            if (banner) {
                banner.classList.add("hidden");
            }
            if (cookieReviewBtn) {
                cookieReviewBtn.classList.remove("hidden");
            }
        });
    }
    
    
    if (cookieReviewBtn) {
        cookieReviewBtn.addEventListener("click", () => {
            banner.classList.remove("hidden");
            cookieReviewBtn.classList.add("hidden");
        })
    }
});

