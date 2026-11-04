//// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.


//const languageSelector = document.getElementById('languageSelector');
//const translations = {
//    en: {
//        home: 'Home',
//        findJobs: 'Find Jobs',
//        companies: 'Companies',
//        about: 'About',
//        profile: 'Profile',
//        addJob: 'Add New Job',
//        myJobs: 'My Jobs',
//        applications: 'Applications',
//        logout: 'Logout',
//        userName: 'John Doe',
//        welcome: 'Welcome to HireUp',
//        description: 'Professional job hiring platform with theme and language support'
//    },
//    ar: {
//        home: 'الرئيسية',
//        findJobs: 'ابحث عن وظائف',
//        companies: 'الشركات',
//        about: 'حول',
//        profile: 'الملف الشخصي',
//        addJob: 'إضافة وظيفة جديدة',
//        myJobs: 'وظائفي',
//        applications: 'الطلبات',
//        logout: 'تسجيل الخروج',
//        userName: 'جون دو',
//        welcome: 'مرحبا بك في HireUp',
//        description: 'منصة احتراف توظيف مع دعم المواضيع واللغات'
//    }
//};

//function initLanguage() {
//    const savedLanguage = localStorage.getItem('language') || 'en';
//    setLanguage(savedLanguage);
//}

//function setLanguage(lang) {
//    localStorage.setItem('language', lang);
//    document.documentElement.lang = lang;
//    document.documentElement.dir = lang === 'ar' ? 'rtl' : 'ltr';

//    // Update all text content
//    const menu = document.querySelectorAll('.navbar-menu a');
//    if (menu[0]) menu[0].textContent = translations[lang].home;
//    if (menu[1]) menu[1].textContent = translations[lang].findJobs;
//    if (menu[2]) menu[2].textContent = translations[lang].companies;
//    if (menu[3]) menu[3].textContent = translations[lang].about;

//    document.querySelector('.user-name').textContent = translations[lang].userName;

//    // Update dropdown menu
//    const dropdownLinks = document.querySelectorAll('.dropdown-menu-custom a');
//    if (dropdownLinks[0]) dropdownLinks[0].innerHTML = `<i class="fas fa-user"></i> ${translations[lang].profile}`;
//    if (dropdownLinks[1]) dropdownLinks[1].innerHTML = `<i class="fas fa-plus-circle"></i> ${translations[lang].addJob}`;
//    if (dropdownLinks[2]) dropdownLinks[2].innerHTML = `<i class="fas fa-briefcase"></i> ${translations[lang].myJobs}`;
//    if (dropdownLinks[3]) dropdownLinks[3].innerHTML = `<i class="fas fa-file-alt"></i> ${translations[lang].applications}`;
//    if (dropdownLinks[5]) dropdownLinks[5].innerHTML = `<i class="fas fa-sign-out-alt"></i> ${translations[lang].logout}`;

//    // Update demo content
//    document.querySelector('.demo-content h1').textContent = translations[lang].welcome;
//    document.querySelector('.demo-content p').textContent = translations[lang].description;

//    // Update language selector UI
//    const langToggle = document.querySelector('.language-toggle');
//    if (lang === 'en') {
//        langToggle.innerHTML = '<span class="language-flag">🇬🇧</span><span class="language-name">EN</span><i class="fas fa-chevron-down language-dropdown-arrow"></i>';
//    } else {
//        langToggle.innerHTML = '<span class="language-flag">🇸🇦</span><span class="language-name">AR</span><i class="fas fa-chevron-down language-dropdown-arrow"></i>';
//    }

//    // Mark active language
//    document.querySelectorAll('.language-menu a').forEach(a => {
//        a.classList.remove('active');
//        if (a.getAttribute('data-lang') === lang) {
//            a.classList.add('active');
//        }
//    });

//    updateStatus(`Language changed to ${lang === 'en' ? 'English' : 'العربية'}`);
//}

//function toggleLanguage(e) {
//    e.stopPropagation();
//    languageSelector.classList.toggle('active');
//}

//function selectLanguage(lang, e) {
//    if (e) {
//        e.preventDefault();
//        e.stopPropagation();
//    }
//    setLanguage(lang);
//    languageSelector.classList.remove('active');
//}

//// Dropdown Management
//function toggleDropdown() {
//    const dropdown = document.getElementById('userDropdown');
//    dropdown.classList.toggle('active');
//}

//function handleAction(action) {
//    updateStatus(`${action} feature coming soon!`);
//    toggleDropdown();
//}

//function navigate(page) {
//    updateStatus(`Navigating to ${page}...`);
//}

//function toggleMenu() {
//    updateStatus('Mobile menu toggled');
//}

//function updateStatus(message) {
//    const statusMessage = document.getElementById('statusMessage');
//    statusMessage.textContent = message;
//    statusMessage.style.opacity = '1';
//    setTimeout(() => {
//        statusMessage.style.opacity = '0.7';
//    }, 2000);
//}

//// Close dropdowns when clicking outside
//document.addEventListener('click', function (event) {
//    const userDropdown = document.getElementById('userDropdown');
//    const langSelector = document.getElementById('languageSelector');

//    if (!userDropdown.contains(event.target)) {
//        userDropdown.classList.remove('active');
//    }
//    if (!langSelector.contains(event.target)) {
//        langSelector.classList.remove('active');
//    }
//}, true);

//// Initialize on page load
//window.addEventListener('load', function () {
//    initTheme();
//    initLanguage();
//});
// ---------------- LANGUAGE SYSTEM ---------------- //

const languageSelector = document.getElementById('languageSelector');

const translations = {
    en: {
        home: 'Home',
        findJobs: 'Find Jobs',
        companies: 'Companies',
        about: 'About',
        profile: 'Profile',
        addJob: 'Add New Job',
        myJobs: 'My Jobs',
        applications: 'Applications',
        logout: 'Logout',
    },
    ar: {
        home: 'الرئيسية',
        findJobs: 'ابحث عن وظائف',
        companies: 'الشركات',
        about: 'حول',
        profile: 'الملف الشخصي',
        addJob: 'إضافة وظيفة جديدة',
        myJobs: 'وظائفي',
        applications: 'الطلبات',
        logout: 'تسجيل الخروج',
    }
};

function initLanguage() {
    const savedLanguage = localStorage.getItem('language') || 'en';
    setLanguage(savedLanguage);
}

function setLanguage(lang) {
    localStorage.setItem('language', lang);

    document.documentElement.lang = lang;
    document.documentElement.dir = lang === 'ar' ? 'rtl' : 'ltr';

    const menu = document.querySelectorAll('.navbar-menu a');
    if (menu[0]) menu[0].textContent = translations[lang].home;
    if (menu[1]) menu[1].textContent = translations[lang].findJobs;
    if (menu[2]) menu[2].textContent = translations[lang].companies;
    if (menu[3]) menu[3].textContent = translations[lang].about;

    const dropdownLinks = document.querySelectorAll('.dropdown-menu-custom a');

    dropdownLinks.forEach(link => {
        if (link.innerHTML.includes('fa-user'))
            link.innerHTML = `<i class="fas fa-user"></i> ${translations[lang].profile}`;

        if (link.innerHTML.includes('fa-plus-circle'))
            link.innerHTML = `<i class="fas fa-plus-circle"></i> ${translations[lang].addJob}`;

        if (link.innerHTML.includes('fa-briefcase'))
            link.innerHTML = `<i class="fas fa-briefcase"></i> ${translations[lang].myJobs}`;

        if (link.innerHTML.includes('fa-file-alt'))
            link.innerHTML = `<i class="fas fa-file-alt"></i> ${translations[lang].applications}`;

        if (link.innerHTML.includes('fa-sign-out-alt'))
            link.innerHTML = `<i class="fas fa-sign-out-alt"></i> ${translations[lang].logout}`;
    });

    const langToggle = document.querySelector('.language-toggle');
    if (langToggle) {
        langToggle.innerHTML = lang === 'en'
            ? `<span class="language-flag">🇬🇧</span> EN <i class="fas fa-chevron-down"></i>`
            : `<span class="language-flag">🇸🇦</span> AR <i class="fas fa-chevron-down"></i>`;
    }

    document.querySelectorAll('.language-menu a').forEach(a => {
        a.classList.remove('active');
        if (a.getAttribute('data-lang') === lang) a.classList.add('active');
    });
}

// ---------------- DROPDOWNS ---------------- //

document.addEventListener('DOMContentLoaded', () => {

    const userDropdown = document.getElementById('userDropdown');
    const langSelector = document.getElementById('languageSelector');

    // Toggle Language Menu
    if (langSelector) {
        const toggleBtn = langSelector.querySelector('.language-toggle');
        toggleBtn?.addEventListener('click', e => {
            e.stopPropagation();
            langSelector.classList.toggle('active');
        });

        langSelector.querySelectorAll('[data-lang]').forEach(item => {
            item.addEventListener('click', e => {
                e.preventDefault();
                setLanguage(item.getAttribute('data-lang'));
                langSelector.classList.remove('active');
            });
        });
    }

    // Toggle User Dropdown
    if (userDropdown) {
        const toggleBtn = userDropdown.querySelector('.dropdown-toggle');
        toggleBtn?.addEventListener('click', e => {
            e.stopPropagation();
            userDropdown.classList.toggle('active');
        });
    }

    // Close dropdowns on outside click
    document.addEventListener('click', () => {
        langSelector?.classList.remove('active');
        userDropdown?.classList.remove('active');
    });

    initLanguage();
});


// ---------------- THEME SYSTEM (Dark Mode) ---------------- //

//function initTheme() {
//    const savedTheme = localStorage.getItem("theme") || "light";
//    setTheme(savedTheme);
//}

//function setTheme(theme) {
//    const root = document.documentElement;

//    if (theme === "dark") {
//        root.classList.add("dark-mode");
//    } else {
//        root.classList.remove("dark-mode");
//    }

//    localStorage.setItem("theme", theme);
//}

//function toggleTheme() {
//    const isDark = document.documentElement.classList.contains("dark-mode");
//    setTheme(isDark ? "light" : "dark");
//}

//document.addEventListener("DOMContentLoaded", () => {
//    const themeBtn = document.getElementById("themeToggle");

//    if (themeBtn) {
//        themeBtn.addEventListener("click", e => {
//            e.stopPropagation();
//            toggleTheme();
//        });
//    }

//    initTheme();
//});
function initTheme() {
    const saved = localStorage.getItem("theme") || "light";
    document.documentElement.setAttribute("data-theme", saved);
}

function toggleTheme() {
    const current = document.documentElement.getAttribute("data-theme") || "light";
    const newTheme = current === "light" ? "dark" : "light";

    document.documentElement.setAttribute("data-theme", newTheme);
    localStorage.setItem("theme", newTheme);
}

document.addEventListener("DOMContentLoaded", () => {
    initTheme();

    const btn = document.getElementById("themeToggle");
    if (btn) {
        btn.addEventListener("click", toggleTheme);
    }
});


