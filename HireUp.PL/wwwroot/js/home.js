
        // Mobile menu toggle
    document.querySelector('.menu-toggle').addEventListener('click', function() {
        document.querySelector('.navbar-menu').classList.toggle('active');
        });

        //// Bookmark toggle
        //document.querySelectorAll('.job-bookmark').forEach(btn => {
        //btn.addEventListener('click', function (e) {
        //    e.preventDefault();
        //    this.classList.toggle('active');
        //    const icon = this.querySelector('i');
        //    icon.classList.toggle('far');
        //    icon.classList.toggle('fas');
        //});
        //});

    // Scroll animations
    const observerOptions = {
        threshold: 0.1,
    rootMargin: '0px 0px -100px 0px'
        };

    const observer = new IntersectionObserver(function(entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
        }, observerOptions);

        document.querySelectorAll('.job-card, .category-card, .testimonial-card').forEach(el => {
        el.style.opacity = '0';
    el.style.transform = 'translateY(20px)';
    el.style.transition = 'all 0.6s ease-out';
    observer.observe(el);
        });
