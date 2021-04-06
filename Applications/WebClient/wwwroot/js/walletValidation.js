

function InitializeWalletValidation() {
    const postaIndexNumberLength = 4;
    const uniqueMasterCitizenNumberLength = 13;

    function initialize() {
        $('#submit-form').click(onSubmitFormClick);
        $('#postal-index-number').keypress(onPostalIndexNumberKeyPress);
        $('#unique-master-citizen-number').keypress(onUniqueMasterCitizenNumberKeyPress);
    }

    function onSubmitFormClick(e) {
        if (document.getElementById('postal-index-number').value.length != postaIndexNumberLength) {
            document.getElementById('postal-index-number-error').innerHTML = 'Invalid postal index number';
            e.preventDefault();
        }
        if (document.getElementById('unique-master-citizen-number').value.length != uniqueMasterCitizenNumberLength) {
            document.getElementById('unique-master-citizen-number-error').innerHTML = 'Invalid unique master citizen number';
            e.preventDefault();
        }
    }

    function onPostalIndexNumberKeyPress(e) {
        const currentLength = document.getElementById('postal-index-number').value.length;
        if (currentLength >= postaIndexNumberLength) {
            document.getElementById('postal-index-number-error').innerHTML = 'Max ' + postaIndexNumberLength + ' digits';
            return false;
        }
        const valid = e.charCode >= 48 && e.charCode <= 57;
        if (!valid) {
            document.getElementById('postal-index-number-error').innerHTML = 'Only digits';
        }
        else if (currentLength != postaIndexNumberLength - 1) {
            document.getElementById('postal-index-number-error').innerHTML = postaIndexNumberLength + ' digits required';
        }
        else {
            document.getElementById('postal-index-number-error').innerHTML = '';
        }
        return valid;
    }

    function onUniqueMasterCitizenNumberKeyPress(e) {
        const currentLength = document.getElementById('unique-master-citizen-number').value.length;

        if (currentLength >= uniqueMasterCitizenNumberLength) {
            document.getElementById('unique-master-citizen-number-error').innerHTML = 'Max ' + uniqueMasterCitizenNumberLength + ' digits';
            return false;
        }
        const valid = e.charCode >= 48 && e.charCode <= 57;
        if (!valid) {
            document.getElementById('unique-master-citizen-number-error').innerHTML = 'Only digits';
        }
        else if (currentLength != uniqueMasterCitizenNumberLength - 1) {
            document.getElementById('unique-master-citizen-number-error').innerHTML = uniqueMasterCitizenNumberLength + ' digits required';
        }
        else {
            document.getElementById('unique-master-citizen-number-error').innerHTML = '';
        }
        return valid;
    }

    $(document).ready(initialize);
}

 
    
 
