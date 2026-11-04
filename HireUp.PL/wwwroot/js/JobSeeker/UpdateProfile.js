document.addEventListener('DOMContentLoaded', function () {
    const avatarUpload = document.getElementById('avatarUpload');
    const avatarWrapper = document.querySelector('.avatar-wrapper');
    const cvFile = document.getElementById('CVPath');
    const cvFileName = document.getElementById('cvFileName');

    if (cvFile) {
        cvFile.addEventListener('change', e => {
            const fileName = e.target.files[0]?.name || '';
            if (cvFileName)
                cvFileName.textContent = fileName ? `📄 ${fileName}` : '';
        });
    }

    if (avatarUpload && avatarWrapper) {
        avatarUpload.addEventListener('change', e => {
            const file = e.target.files[0];
            if (!file) return;

            const reader = new FileReader();
            reader.onload = ev => {
                avatarWrapper.innerHTML = '';
                const img = document.createElement('img');
                img.src = ev.target.result;
                img.style.cssText = `
                    width:100%;
                    height:100%;
                    border-radius:50%;
                    object-fit:cover;
                `;
                avatarWrapper.appendChild(img);
                avatarWrapper.appendChild(avatarUpload);
            };
            reader.readAsDataURL(file);
        });
    }
});
