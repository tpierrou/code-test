const constructListComponent = async (root, data = null) => {
    if (data == null) {
        const resp = await fetch("./items");
        data = await resp.json();
    }

    const renderItem = (item) => `
        <li>
            <input type="checkbox" name="items" id="${item.id}" value="${item.id}" />
            <label for="${item.id}">
                <div>${item.name}</div>
                <div>${item.description}</div>
            </label>
        </li>`;

    const render = () => {
        let html = data.map(d => renderItem(d)).join("");

        root.innerHTML = html;
    };

    render();
}

const submitSelectedItems = async (event) => {
    event.preventDefault(); // Prevent the default form submission

    const checkedCheckboxes = document.querySelectorAll("input[name='items']:checked");
    const formData = new FormData();
    checkedCheckboxes.forEach(cb => formData.append("items", cb.id));

    try {
        // Send a POST request with the selected items
        const response = await fetch("./items", {
            method: "POST",
            body: formData
        });

        if (response.ok) {
            const items = await response.json();
            constructListComponent(document.querySelector("ul"), items);
        } else {
            alert("Failed to submit items. Please try again.");
        }
    } catch (error) {
        alert("An error occurred while submitting items. Please try again.");
    }
}

constructListComponent(document.querySelector("ul"));
document.querySelector("form").addEventListener("submit", submitSelectedItems);
