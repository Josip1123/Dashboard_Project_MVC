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
                window.location.href = '/Dashboard/Projects';
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
                window.location.href = '/Dashboard/Projects';
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
            deleteDarkModeCookie();
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
    
    
    /*
    Add Members 
    */
    const addMemberButton = document.querySelector("#addMemberBtn")
    const addMemberForm = document.querySelector("#addMemberForm")
    const memberFormCloseBtn = document.querySelector(".member-form-close-btn")
    const editMemberClose = document.querySelector("#EditMemberClose")
    const memberEditMenus = document.querySelectorAll(".member-edit-menu")
    
    if(addMemberButton) {
        addMemberButton.addEventListener("click", ()=>{
            addMemberForm.classList.remove("hidden");
        })
    }

    if(memberFormCloseBtn){
        memberFormCloseBtn.addEventListener("click", ()=>{
            window.location.href = '/Dashboard/Members';
        })
    }
    

    if (editMemberClose){
        editMemberClose.addEventListener("click", ()=>{
            window.location.href = '/Dashboard/Members';
        });
    }

    if(memberEditMenus) {
        memberEditMenus.forEach(btn => {
            btn.addEventListener("click", (e) => {
                e.stopPropagation();

                const card = btn.closest(".member-card-container");
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
    
    const darkModeBtn = document.querySelector(".darkmode-container");
    const darkModeIcon = document.querySelector(".darkmode-img");
    const logo = document.querySelector(".logo");
    const notificationIcon = document.querySelector(".notification-icon")
    
    if (darkModeBtn){
        darkModeBtn.addEventListener("click", ()=>{
            document.body.classList.toggle('darkmode');
            updateDarkmodeIcon();
        })
    }
    
    function updateDarkmodeIcon() {
        
        if (document.body.classList.contains("darkmode")){
            darkModeIcon.src = "/img/sun-svgrepo-com.svg";
            logo.src = "/img/full-darkmode.svg";
            notificationIcon.src = "/img/Notification-dark.svg"
            setDarkModeCookie(true);
            
        } else {
            darkModeIcon.src = "/img/moon-svgrepo-com.svg";
            logo.src = "/img/full.svg";
            notificationIcon.src = "/img/Notification.svg";
            setDarkModeCookie(false);
        } 
    }
    
    
    /*cookie handler med hjälp av chatGPT*/
    
    function setDarkModeCookie(enable) {
        const maxAge = 60 * 60 * 24 * 365;
        document.cookie = [
            'darkmode=' + (enable ? 'true' : 'false'),
            'path=/',
            'max-age=' + maxAge,
            'SameSite=Lax'
        ].join('; ');
    }

    function deleteDarkModeCookie() {
        document.cookie = [
            'darkmode=',
            'path=/',
            'max-age=0',
            'SameSite=Lax'
        ].join('; ');
    }
    
    /*Image preview*/
    const memberImgField = document.querySelector('#ImageFile');
    const memberImgEditField = document.querySelector('#ImageEditFile');
    
    if (memberImgField) {
        memberImgField.addEventListener('change', function (e) {
            const file = e.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('imgPreview').src = e.target.result;
                };
                reader.readAsDataURL(file);
            }
        });
    }

    if (memberImgEditField) {
        memberImgEditField.addEventListener('change', function (e) {
            const file = e.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('imgEditPreview').src = e.target.result;
                };
                reader.readAsDataURL(file);
            }
        });
    }
    
    updateDarkmodeIcon();
});



