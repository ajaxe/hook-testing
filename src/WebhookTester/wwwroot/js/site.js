// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

let DateTime = luxon.DateTime;
$(function () {
  const toastElList = document.querySelectorAll(".toast");
  const toastList = [...toastElList].map(
    (toastEl) => new bootstrap.Toast(toastEl)
  );

  initializeUrlCopy();

  initCallbackListUi();

  htmxResponseErrorHandler();

  fixDateDisplay();
});

function initCallbackListUi() {
  const $callbackList = $("#callback-list");
  $callbackList.find("li:first-child").addClass("active");
  $callbackList.find("li").on("click", function () {
    $(this).parent().find(".active").removeClass("active");
    $(this).addClass("active");
  });

  resizeCallbackList($callbackList);
}

function resizeCallbackList($callbackList) {
  function resize() {
    var htOffset =
      $("#callback-detail > h5").outerHeight() * 5 +
      $("header").outerHeight() +
      $("#session-detail").outerHeight();
    $callbackList
      .parent()
      .css("max-height", "calc(100vh - " + htOffset + "px)")
      .css("overflow", "auto");

    console.log("htOffset: " + htOffset);
  }

  resize();

  let resizeTimeout = 0;

  $(window).on("resize", function () {
    console.log("resize event");
    clearTimeout(resizeTimeout);
    resizeTimeout = setTimeout(resize, 200);
  });
}

function initializeUrlCopy() {
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
}

function htmxResponseErrorHandler() {
  $(document).on("htmx:responseError", function (e) {
    const errModel = JSON.parse(e.detail.xhr.responseText);
    console.log(errModel);
    setGlobalToast(errModel.errorMessage, true);
  });
}

function fixDateDisplay() {
  for (const elem of $(".need-dt-fix")) {
    let $elem = $(elem);
    let dt = $elem.text().trim();
    let iso = DateTime.fromISO(dt);
    let formatted = iso.toFormat("h:mm a 'on' MMM d, yyyy");
    $elem.empty().text(formatted).removeClass("d-none need-dt-fix");
  }
}
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
