
        // State
    let skills = [];

// DOM Elements
const jobForm = document.getElementById('jobForm');
const skillInput = document.getElementById('Skills');
const addSkillBtn = document.getElementById('addSkillBtn');
const skillsList = document.getElementById('skillsList');

const salaryMin = document.getElementById('SalaryMin');
const salaryMax = document.getElementById('SalaryMax');
const salaryDisplay = document.getElementById('salaryDisplay');
const successMessage = document.getElementById('successMessage');


    // Update salary display
    function updateSalaryDisplay() {
            const min = salaryMin.value || '0';
    const max = salaryMax.value || '0';
    salaryDisplay.textContent = `$${parseInt(min).toLocaleString()} - $${parseInt(max).toLocaleString()}`;
        }

    salaryMin.addEventListener('input', updateSalaryDisplay);
    salaryMax.addEventListener('input', updateSalaryDisplay);

    // Add skill
    function addSkill() {
            const skill = skillInput.value.trim();

    if (!skill) {
        skillInput.focus();
    return;
            }

    if (skills.includes(skill)) {
        alert('This skill is already added!');
    return;
            }

    skills.push(skill);
    skillInput.value = '';
    renderSkills();
    skillInput.focus();
        }

    // Remove skill
    function removeSkill(skill) {
        skills = skills.filter(s => s !== skill);
    renderSkills();
        }

    // Render skills
    function renderSkills() {
        skillsList.innerHTML = '';
            skills.forEach(skill => {
                const badge = document.createElement('div');
    badge.className = 'skill-badge';
    badge.innerHTML = `
    <span>${skill}</span>
    <button type="button" data-skill="${skill}">
        <i class="fas fa-times"></i>
    </button>
    `;
                badge.querySelector('button').addEventListener('click', (e) => {
        e.preventDefault();
    removeSkill(skill);
                });
    skillsList.appendChild(badge);
            });
        }

        // Add skill button click
        addSkillBtn.addEventListener('click', (e) => {
        e.preventDefault();
    addSkill();
        });

        // Add skill on Enter
        skillInput.addEventListener('keypress', (e) => {
            if (e.key === 'Enter') {
        e.preventDefault();
    addSkill();
            }
        });

        // Form submission
        jobForm.addEventListener('submit', () => {

            document.getElementById("SkillsHidden").value = JSON.stringify(skills);

    // Validation
    if (parseInt(salaryMax.value) < parseInt(salaryMin.value)) {
        alert('Maximum salary must be greater than minimum salary!');
    return;
            }

    // Collect form data
    const formData = {
        title: document.getElementById('Title').value,
    description: document.getElementById('Description').value,
    requirements: document.getElementById('Requirements').value,
    salaryMin: parseFloat(salaryMin.value),
    salaryMax: parseFloat(salaryMax.value),
    type: document.getElementById('Type').value,
    experienceLevel: document.getElementById('ExperienceLevel').value,
    skills: skills,
    country: document.getElementById('Country').value,
    location: document.getElementById('Location').value
            };

    // Log form data (in real app, send to server)
    console.log('Job Posted:', formData);

    // Show success message
    successMessage.classList.add('show');
            setTimeout(() => {
        successMessage.classList.remove('show');
            }, 3000);

    // Reset form
    jobForm.reset();
    skills = [];
    renderSkills();
    updateSalaryDisplay();
        });

    // Initial salary display
    updateSalaryDisplay();
