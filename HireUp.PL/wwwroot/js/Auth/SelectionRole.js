let selectedCard = null;

function chooseRole(role, card) {
    document.getElementById("roleInput").value = role;

    // Styling for selected card
    if (selectedCard) selectedCard.classList.remove("selected");
    selectedCard = card;
    card.classList.add("selected");

    enableContinue();
}

document.getElementById("terms").addEventListener("change", enableContinue);

function enableContinue() {
    const btn = document.getElementById("continueBtn");
    const role = document.getElementById("roleInput").value;
    const terms = document.getElementById("terms").checked;

    btn.disabled = !(role && terms);
}

// Submit form
document.getElementById("continueBtn").addEventListener("click", function () {
    document.getElementById("roleForm").submit();
});