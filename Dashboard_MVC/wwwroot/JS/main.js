const addProjectForm = document.querySelector("#projectForm");
const addProjectButton = document.querySelector("#addProjectBtn");
const projectFormCloseBtn = document.querySelector("#projectFormClose");
const projectMenuBtns = document.querySelectorAll(".project-menu-btn");
const editMenus = document.querySelectorAll(".edit-menu");
const editClose = document.querySelector("#editProjectFormClose");
const editForm = document.querySelector("#editProjectForm");
const editBtn = document.querySelector(".edit-btn");

editBtn.addEventListener("click",()=>{
    editForm.classList.remove("hidden")
} )


editClose.addEventListener("click", ()=>{
    editForm.classList.add("hidden");
})


addProjectButton.addEventListener("click", ()=>{
    addProjectForm.classList.remove("hidden");
})

projectFormCloseBtn.addEventListener("click", ()=>{
    addProjectForm.classList.add("hidden");
})

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