function openForm() {
    document.getElementById("popupBox").style.display = "block";
}

function closeForm() {
    document.getElementById("popupBox").style.display = "none";
}

const stereg = ['ArrowUp', 'ArrowUp', 'ArrowDown', 'ArrowDown', 'ArrowLeft', 'ArrowRight', 'ArrowLeft', 'ArrowRight', 'b', 'a'];

let ind = 0;

window.addEventListener('keydown', (event) => {
    if (event.key === stereg[ind]) {
        ind++;
        if (ind === stereg.length) {
            console.log('Test Mode');
            $("#test").removeAttr("hidden");
            ind = 0;
        }
    }
    else {
        ind = 0;
    }
});