const addProjectForm = document.querySelector("#projectForm");
const addProjectButton = document.querySelector("#addProjectBtn");
const projectFormCloseBtn = document.querySelector("#projectFormClose");

addProjectButton.addEventListener("click", ()=>{
    addProjectForm.classList.remove("hidden");
})

projectFormCloseBtn.addEventListener("click", ()=>{
    addProjectForm.classList.add("hidden");
})

