
    function toggleDropdown() {
            const dropdown = document.getElementById('userDropdown');
    dropdown.classList.toggle('active');
        }

    function handleAction(action) {
        console.log('Action clicked:', action);
    alert(`${action} feature coming soon!`);
    toggleDropdown();
        }

    function toggleMenu() {
        console.log('Mobile menu toggled');
        }

    // Close dropdown when clicking outside
    document.addEventListener('click', function(event) {
            const dropdown = document.getElementById('userDropdown');
    if (!dropdown.contains(event.target)) {
        dropdown.classList.remove('active');
            }
        });

