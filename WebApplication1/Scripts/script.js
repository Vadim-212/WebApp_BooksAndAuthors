$(document).ready(function()
{
    $(`#survey-submit-btn`).on(`click`, function (e)
    {
        if ($(`[name=surveyAgeRadios]:checked`).length == 0)
            return;
        if ($(`[name=surveyBookReadRadios]:checked`).length == 0)
            return;
        if ($(`[name=bookGenreChecks]:checked`).length == 0)
            return;
    })
})