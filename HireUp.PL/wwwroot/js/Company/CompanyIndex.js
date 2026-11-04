
        // Companies data
    const companies = [
    {id: 1, name: "TechCorp Solutions", logo: "T", industry: "Technology", size: "500-1000", location: "Cairo, Egypt", description: "Leading software development company specializing in enterprise solutions", rating: 4.5, reviews: 128, employees: "750" },
    {id: 2, name: "Digital Innovations", logo: "D", industry: "Technology", size: "100-500", location: "Alexandria, Egypt", description: "Innovative tech startup building next-generation digital products", rating: 4.8, reviews: 89, employees: "280" },
    {id: 3, name: "Creative Studio", logo: "C", industry: "Design", size: "50-100", location: "Remote", description: "Award-winning design agency creating stunning digital experiences", rating: 4.7, reviews: 64, employees: "72" },
    {id: 4, name: "StartupX", logo: "S", industry: "Business", size: "100-500", location: "Cairo, Egypt", description: "Fast-growing SaaS startup revolutionizing business processes", rating: 4.6, reviews: 102, employees: "320" },
    {id: 5, name: "Growth Agency", logo: "G", industry: "Marketing", size: "50-100", location: "Remote", description: "Performance-driven marketing agency helping brands scale", rating: 4.4, reviews: 76, employees: "85" },
    {id: 6, name: "AI Labs", logo: "A", industry: "Technology", size: "200-500", location: "Cairo, Egypt", description: "Cutting-edge AI research and development company", rating: 4.9, reviews: 145, employees: "380" },
    {id: 7, name: "HealthTech Inc", logo: "H", industry: "Healthcare", size: "1000+", location: "Cairo, Egypt", description: "Healthcare technology leader improving patient outcomes", rating: 4.3, reviews: 198, employees: "1200" },
    {id: 8, name: "EduConnect", logo: "E", industry: "Education", size: "200-500", location: "Remote", description: "Online education platform connecting learners with experts", rating: 4.6, reviews: 112, employees: "450" },
    {id: 9, name: "Finance Pro", logo: "F", industry: "Business", size: "500-1000", location: "Cairo, Egypt", description: "Fintech company providing innovative financial solutions", rating: 4.5, reviews: 135, employees: "680" },
    {id: 10, name: "Design Co", logo: "D", industry: "Design", size: "50-100", location: "Alexandria, Egypt", description: "Creative design house specializing in branding and identity", rating: 4.7, reviews: 58, employees: "95" },
    {id: 11, name: "DevHub", logo: "D", industry: "Technology", size: "100-500", location: "Remote", description: "Developer-first platform and community hub for coders", rating: 4.8, reviews: 167, employees: "240" },
    {id: 12, name: "MediaStream", logo: "M", industry: "Marketing", size: "200-500", location: "Cairo, Egypt", description: "Digital media production and content creation company", rating: 4.4, reviews: 91, employees: "320" }
    ];

    // State
    let currentSearch = '';
    let currentIndustry = 'Technology';

    // DOM Elements
    const searchInput = document.getElementById('searchInput');
    const searchBtn = document.getElementById('searchBtn');
    const industryPills = document.querySelectorAll('.industry-pill');
    const companiesGrid = document.getElementById('companiesGrid');
    const noResults = document.getElementById('noResults');
    const companiesHeading = document.getElementById('companiesHeading');

    // Render companies
    function renderCompanies() {
            const filtered = companies.filter(company => {
                const matchesSearch = company.name.toLowerCase().includes(currentSearch.toLowerCase()) ||
    company.description.toLowerCase().includes(currentSearch.toLowerCase());
    const matchesIndustry = company.industry === currentIndustry;

    return matchesSearch && matchesIndustry;
            });

    companiesGrid.innerHTML = '';

    if (filtered.length === 0) {
        noResults.classList.add('show');
    companiesHeading.textContent = 'No Results';
    return;
            }

    noResults.classList.remove('show');
    companiesHeading.textContent = `${currentIndustry} Companies (${filtered.length})`;

            filtered.forEach(company => {
                const card = document.createElement('div');
    card.className = 'company-card';

    card.innerHTML = `
    <div class="company-header">
        <div>
            <div class="company-logo">${company.logo}</div>
            <h3 class="company-name">${company.name}</h3>
            <span class="company-industry">${company.industry}</span>
            <div class="company-rating">
                <i class="fas fa-star"></i>
                <span class="company-rating-value">${company.rating}</span>
                <span class="company-rating-count">(${company.reviews})</span>
            </div>
        </div>
    </div>
    <p class="company-description">${company.description}</p>
    <div class="company-meta">
        <div class="company-meta-item">
            <i class="fas fa-map-marker-alt"></i>
            <span><strong class="company-meta-label">Location:</strong> ${company.location}</span>
        </div>
        <div class="company-meta-item">
            <i class="fas fa-users"></i>
            <span><strong class="company-meta-label">Employees:</strong> ${company.employees}</span>
        </div>
    </div>
    <div class="company-footer">
        <button class="btn-visit"><i class="fas fa-external-link-alt"></i> Visit Website</button>
    </div>
    `;

                // Visit button
                card.querySelector('.btn-visit').addEventListener('click', () => {
        alert(`Visiting ${company.name} website`);
                });

    companiesGrid.appendChild(card);
            });
        }

    // Search functionality
    function performSearch() {
        currentSearch = searchInput.value.trim();
    renderCompanies();
        }

    // Search button click
    searchBtn.addEventListener('click', performSearch);

        // Search on Enter
        searchInput.addEventListener('keypress', (e) => {
            if (e.key === 'Enter') {
        performSearch();
            }
        });

        // Industry pill selection
        industryPills.forEach(pill => {
        pill.addEventListener('click', function () {
            // Remove active from all pills
            industryPills.forEach(p => p.classList.remove('active'));

            // Add active to clicked pill
            this.classList.add('active');

            // Update current industry
            currentIndustry = this.getAttribute('data-industry');

            // Clear search when switching industries
            currentSearch = '';
            searchInput.value = '';

            // Filter companies
            renderCompanies();
        });
        });

    // Initial render
    renderCompanies();
