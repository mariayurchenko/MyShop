var page = (function (window, document) {
    
	/************************************************************************************
    * Variables
    ************************************************************************************/
    var formContext;

    var layout = {
        IsRequired: false,
        BpfPrefix: "header_process_",
        Fields: {
            CustomerNeed: "customerneed"
        },
        StagesName: {
            Propose: "Propose"
        },
        StageFields: {

            //# Propose stage
            IdentifyPursuItteam: "identifypursuitteam",
            CustomerNeed: "customerneed_1",
            DevelopProposal: "developproposal",
            CompleteInternalReview: "completeinternalreview",
            //# end Propose stage

        }
    };

    /************************************************************************************
    * Page events
    ************************************************************************************/

    function onLoad(context) {
        try {
            formContext = context.getFormContext();

            onIdentifyPursuItteamChange();

            getAttribute(layout.StageFields.IdentifyPursuItteam).addOnChange(function (executionContext) {
                onIdentifyPursuItteamChange();
            });
            getAttribute(layout.Fields.CustomerNeed).addOnChange(function (executionContext) {
                onCustomerNeedChange();
            });
            getAttribute(layout.StageFields.DevelopProposal).addOnChange(function (executionContext) {
                onDevelopProposalChange();
            });
            getAttribute(layout.StageFields.CompleteInternalReview).addOnChange(function (executionContext) {
                onCompleteInternalReviewChange();
            });
            

            formContext.data.process.addOnStageChange(controlOnActiveStage);

        } catch (e) {
            console.log(e);
            showErrorMessage(e.message);
        }
    };

    /************************************************************************************
    * Form events
    ************************************************************************************/


    function controlOnActiveStage() {

        var stages = formContext.data.process.getActiveProcess().getStages();
        if (stages != null) {
            stages.forEach((stage) => {
                var stageName = stage.getName();
                switch (stageName) {
                case layout.StagesName.Propose:
                    controlFieldsOnProposeStage(stage);
                    break;

                default:
                    break;
                }
            });
        }
    }

    function controlFieldsOnProposeStage(stage) {
        stage.getSteps().forEach(function(field) {
            var fieldName = field.getAttribute();
            switch (fieldName) {
            case layout.StageFields.CustomerNeed:
                break;
            case layout.StageFields.CompleteInternalReview:
                break;
            case layout.StageFields.DevelopProposal:
                break;
            case layout.StageFields.IdentifyPursuItteam:
                break;
            default:
                break;
            }
        });
    }

    /************************************************************************************
     * Field events
     ************************************************************************************/

    function onIdentifyPursuItteamChange() {
        var identifyPursuItteam = getValue(layout.StageFields.IdentifyPursuItteam);

        if (identifyPursuItteam === false || identifyPursuItteam === null) {
            hideField(layout.StageFields.CustomerNeed);
            hideField(layout.StageFields.DevelopProposal);
            hideField(layout.StageFields.CompleteInternalReview);
            noRequiredField(layout.Fields.CustomerNeed);
            unlockFieldToChange(layout.StageFields.CustomerNeed);
            setValue(layout.StageFields.DevelopProposal, false);
            setValue(layout.StageFields.CompleteInternalReview, false);
        }
        else {
            showField(layout.StageFields.CustomerNeed);
            showField(layout.StageFields.DevelopProposal);
            showField(layout.StageFields.CompleteInternalReview);
        }
    }

    function onCustomerNeedChange() {
        if (getValue(layout.Fields.CustomerNeed) === null && layout.IsRequired) {
            hideField(layout.StageFields.CompleteInternalReview);
        }
        else {
            showField(layout.StageFields.CompleteInternalReview);
        }
    }

    function onDevelopProposalChange() {
        if (getValue(layout.StageFields.DevelopProposal) === false) {
            noRequiredField(layout.Fields.CustomerNeed);
            layout.IsRequired = false;
            showField(layout.StageFields.CompleteInternalReview);
        }
        else {
            requiredField(layout.Fields.CustomerNeed);
            layout.IsRequired = true;
            if (getValue(layout.Fields.CustomerNeed) === null) {
                hideField(layout.StageFields.CompleteInternalReview);
            }
            else {
                showField(layout.StageFields.CompleteInternalReview);
            }
        }
    }

    function onCompleteInternalReviewChange() {
        if (getValue(layout.StageFields.CompleteInternalReview) === false) {
            unlockFieldToChange(layout.StageFields.CustomerNeed);
            showField(layout.StageFields.DevelopProposal);
        }
        else {
            lockFieldToChange(layout.StageFields.CustomerNeed);

            if (getValue(layout.Fields.CustomerNeed) === null) {
                hideField(layout.StageFields.DevelopProposal);
            }
            else {
                showField(layout.StageFields.DevelopProposal);
            }
        }
    }


    /************************************************************************************
    * Helpers
    ************************************************************************************/

    function hideField(field) { 
        /// <summary> 
        ///    hide field
        ///</summary >
        ///
        formContext.getControl(layout.BpfPrefix + field).setVisible(false);
    }

    function showField(field) {
        /// <summary> 
        ///    show field
        ///</summary >
        ///
        formContext.getControl(layout.BpfPrefix + field).setVisible(true);
    }

    function requiredField(field) { 
        /// <summary> 
        ///    make field required
        ///</summary >
        ///
        getAttribute(field).setRequiredLevel("required");
    }

    function noRequiredField(field) { 
        /// <summary> 
        ///    make field no required
        ///</summary >
        ///
        getAttribute(field).setRequiredLevel("none");
    }

    function getValue(field) { 
        /// <summary> 
        ///    get field's value
        ///</summary >
        ///
        return (getAttribute(field).getValue());
    }

    function setValue(field, value) { 
        /// <summary> 
        ///    set field's value
        ///</summary >
        ///
        getAttribute(field).setValue(value);
    }

    function lockFieldToChange(field) {
        /// <summary> 
        ///    lock field to change
        ///</summary >
        ///
        formContext.getControl(layout.BpfPrefix + field).setDisabled(true);
    }

    function unlockFieldToChange(field) {
        /// <summary> 
        ///   unlock field to change
        ///</summary >
        ///
        formContext.getControl(layout.BpfPrefix + field).setDisabled(false);
    }

    function getAttribute(attributeName) {
        /// <summary> Returns attribute. </summary>
        /// <param name="attributeName" type="string"> Attribute name. </param>
        /// <returns type="Object"> Return the attribute. </returns>

        var attribute = formContext.getAttribute(attributeName);
        if (attribute == null) {
            throw new Error("Data Field " + attributeName + " not found.");
        }

        return attribute;
    }

    function showErrorMessage(message) {

        var guid = Date.now().toString();

        formContext.ui.setFormNotification(message, "ERROR", guid);

        setTimeout(clearMessage, 5000, guid);
    }

    function clearMessage(guid) {
        try {

            formContext.ui.clearFormNotification(guid);

        } catch (e) {
            Xrm.Navigation.openAlertDialog({confirmButtonLabel: "Ok", text: e.message});
        }
    }

    return {
        onLoad: onLoad
    };
})(window, document);