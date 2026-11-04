
        // State management
    let currentCategory = 'Technology';
    let searchQuery = '';

    // DOM Elements
    const searchInput = document.getElementById('searchInput');
    const searchBtn = document.getElementById('searchBtn');
    const searchError = document.getElementById('searchError');
    const categoryPills = document.querySelectorAll('.category-pill');
    const jobCards = document.querySelectorAll('.job-card');
    const jobsHeading = document.getElementById('jobsHeading');
    const noResults = document.getElementById('no-results');

    // Search functionality
    function performSearch() {
        searchQuery = searchInput.value.trim().toLowerCase();

            // Validation
            if (searchQuery.length > 0 && searchQuery.length < 2) {
        searchError.textContent = 'Please enter at least 2 characters';
    searchError.classList.add('show');
    return;
            }

    searchError.classList.remove('show');
    filterJobs();
        }

    // Filter jobs based on category and search
    function filterJobs() {
        let visibleCount = 0;
            
            jobCards.forEach(card => {
                const category = card.getAttribute('data-category');
    const title = card.getAttribute('data-title').toLowerCase();
    const company = card.getAttribute('data-company').toLowerCase();
    const location = card.getAttribute('data-location').toLowerCase();

    const matchesCategory = category === currentCategory;
    const matchesSearch = searchQuery === '' ||
    title.includes(searchQuery) ||
    company.includes(searchQuery) ||
    location.includes(searchQuery);

    if (matchesCategory && matchesSearch) {
        card.classList.remove('hidden');
    visibleCount++;
                } else {
        card.classList.add('hidden');
                }
            });

    // Show/hide no results message
    if (visibleCount === 0) {
        noResults.classList.add('show');
            } else {
        noResults.classList.remove('show');
            }

    // Update heading
    updateHeading(visibleCount);
        }

    // Update jobs heading
    function updateHeading(count) {
            if (searchQuery) {
        jobsHeading.textContent = `Search Results (${count})`;
            } else {
        jobsHeading.textContent = `${currentCategory} Jobs (${count})`;
            }
        }

    // Search button click
    searchBtn.addEventListener('click', performSearch);

        // Search on Enter key
        searchInput.addEventListener('keypress', (e) => {
            if (e.key === 'Enter') {
        performSearch();
            }
        });

        // Clear error on input
        searchInput.addEventListener('input', () => {
            if (searchError.classList.contains('show')) {
        searchError.classList.remove('show');
            }
        });

        // Category selection
        categoryPills.forEach(pill => {
        pill.addEventListener('click', function () {
            // Remove active from all pills
            categoryPills.forEach(p => p.classList.remove('active'));

            // Add active to clicked pill
            this.classList.add('active');

            // Update current category
            currentCategory = this.getAttribute('data-category');

            // Clear search when switching categories
            searchQuery = '';
            searchInput.value = '';

            // Filter jobs
            filterJobs();
        });
        });

        // Bookmark functionality
        document.querySelectorAll('.bookmark-btn').forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.stopPropagation();
            this.innerHTML = this.innerHTML.includes('far') ?
                '<i class="fas fa-bookmark"></i>' :
                '<i class="far fa-bookmark"></i>';
            this.classList.toggle('bookmarked');
        });
        });

        // Apply button functionality
        document.querySelectorAll('.btn-apply').forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.stopPropagation();
            const jobCard = this.closest('.job-card');
            const jobTitle = jobCard.querySelector('.job-title').textContent;
            alert(`Application submitted for: ${jobTitle}`);
        });
        });

        // Job card click (expand/view details)
        jobCards.forEach(card => {
        card.addEventListener('click', function (e) {
            // Don't trigger if clicking buttons
            if (e.target.closest('.bookmark-btn') || e.target.closest('.btn-apply')) {
                return;
            }
            const jobTitle = this.querySelector('.job-title').textContent;
            console.log('View details for:', jobTitle);
        });
        });

    // Initialize - show Technology jobs by default
    filterJobs();
