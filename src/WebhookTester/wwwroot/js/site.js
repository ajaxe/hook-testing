// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
  const toastElList = document.querySelectorAll(".toast");
  const toastList = [...toastElList].map(
    (toastEl) => new bootstrap.Toast(toastEl)
  );

  $("[data-urlpart]").on("click", function () {
    let $target = $(this);
    console.log($target.data("urlpart"));
    let $icon = $target.find("i.bi");
    let orignalCss = $icon.attr("class");
    let effectCss = "bi bi-check-lg fw-semibold";
    $icon.removeClass(orignalCss).addClass(effectCss);
    $target.removeClass("text-primary").addClass("text-success copy-fadeout");
    let urlpart = $target.data("urlpart");
    navigator.clipboard.writeText(location.origin + urlpart);
    setTimeout(function () {
      $icon.removeClass(effectCss).addClass(orignalCss);
      $target.removeClass("text-success copy-fadeout").addClass("text-primary");
    }, 2000);
  });

  $("#callback-list").find("li:first-child").addClass("active");
  $("#callback-list")
    .find("li")
    .on("click", function () {
      $(this).parent().find(".active").removeClass("active");
      $(this).addClass("active");
    });

  $(document).on("htmx:responseError", function (e) {
    const errModel = JSON.parse(e.detail.xhr.responseText);
    console.log(errModel);
    setGlobalToast(errModel.errorMessage, true);
  });
});

function setGlobalToast(message, isError) {
  const css = isError ? "text-bg-danger" : "text-bg-primary";
  const $toastElem = $("#global-toast");
  const globalToast = bootstrap.Toast.getInstance($toastElem[0]);

  $toastElem.addClass(css).find(".toast-message").text(message);
  globalToast.show();

  $toastElem.one("shown.bs.toast", function () {
    $(this).removeClass(css);
  });
}
