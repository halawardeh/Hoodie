document.getElementById("storeSearchButton").addEventListener("click", function() {
    let query = document.getElementById("storeSearchInput").value.toLowerCase();
    let stores = document.querySelectorAll(".store-card");
    stores.forEach(store => {
        let storeName = store.querySelector("h3").innerText.toLowerCase();
        store.style.display = storeName.includes(query) ? "block" : "none";
    });
});

document.querySelectorAll(".store-filter-btn").forEach(button => {
    button.addEventListener("click", function() {
        let category = this.getAttribute("data-category");
        let stores = document.querySelectorAll(".store-card");
        stores.forEach(store => {
            store.style.display = (category === "all" || store.getAttribute("data-category") === category) ? "block" : "none";
        });
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const sections = document.querySelectorAll(".store-sections h2");
    sections.forEach((section) => {
        section.addEventListener("click", () => {
            alert("This section will be sorted dynamically!");
        });
    });
});
