/* idee för detta kom från ChatGPT anpassat för min behov, 
jag ville ha "universal" form validator, tänkte denna såg rolig ut.
 Uses patterns för olika forms och fetches rätt element och span att displaya error*/

const memberPatterns = {
    "CreateMember.Email": {
        regex: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
        empty: "E‑mail is required",
        invalid: "Invalid e‑mail address"
    },
    "CreateMember.FirstName": {
        empty: "First name is required",
    },
    "CreateMember.LastName": {
        empty: "Last name is required",
    },
    "CreateMember.Title": {
        empty: "Title is required",
    }
};

const projectPatterns = {
    "CreateProject.ProjectName": {
        empty: "Name is required",
    },
    "CreateProject.ClientName": {
        empty: "Name is required",
    },
    "CreateProject.Description": {
        regex: /^[\s\S]{0,120}$/,
        empty: "Description is required",
        invalid: "Too long max 150 char."
    }
};

const editMemberPatterns = {
    "EditMember.Email": {
        regex: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
        empty: "E‑mail is required",
        invalid: "Invalid e‑mail address"
    },
    "EditMember.FirstName": {
        empty: "First name is required",
    },
    "EditMember.LastName": {
        empty: "Last name is required",
    },
    "EditMember.Title": {
        empty: "Title is required",
    }
};

const editProjectPatterns = {
    "EditProject.ProjectName": {
        empty: "Name is required",
    },
    "EditProject.ClientName": {
        empty: "Name is required",
    },
    "EditProject.Description": {
        regex: /^[\s\S]{0,120}$/,
        empty: "Description is required",
        invalid: "Too long max 120 char."
    }
};

const loginPatterns = {
    "Email": {
        regex: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
        empty: "E‑mail is required",
        invalid: "Invalid e‑mail address"
    },
    "Password": {
        empty: "Password is required",
    }
};

const regPatterns = {
    "FirstName": {
        empty: "First name is required",
    },
    "LastName": {
        empty: "Last name is required",
    },
    "Email": {
        regex: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
        empty: "E‑mail is required",
        invalid: "Invalid e‑mail address"
    },
    "Password": {
        regex: /(?=.*\d)(?=.*[^\w\s]).{8,}/,
        empty: "Password is required",
        invalid: "Invalid password format"
    }
};



function validateForm(formSelector, patterns) {
    
    const form = document.querySelector(formSelector);
    if (!form) return;

   
    form.addEventListener("input", ({ target }) => {
        if (patterns[target.name]) {
            validate(target);
        }
    });

    form.addEventListener("submit", event => {
        for (const pattern in patterns) {
            const fieldElement = form.querySelector(`[name="${pattern}"]`);
            if (fieldElement && !validate(fieldElement)) {
                event.preventDefault();
            }
        }
    });

    function validate(element) {
        const value = element.value.trim();
        const pattern = patterns[element.name];
        let message = "";

        if (value === "") {
            message = pattern.empty;
        } else if (pattern.regex && !pattern.regex.test(value)) {
            message = pattern.invalid;
        }
        element.nextElementSibling.textContent = message;
        return message === "";
    }
}

validateForm("#addMember", memberPatterns);
validateForm("#addProject", projectPatterns);
validateForm("#editProject", editProjectPatterns);
validateForm("#editMember", editMemberPatterns);
validateForm(".form-admin", loginPatterns);
validateForm("#userLoginForm", loginPatterns);
validateForm("#regForm", regPatterns);


