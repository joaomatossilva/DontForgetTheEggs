module.exports = {
    jsFilesAfterBody: [
        'node_modules/admin-lte/plugins/jquery/jquery.min.js',
        'node_modules/admin-lte/plugins/bootstrap/js/bootstrap.min.js',
        'node_modules/admin-lte/plugins/select2/select2.full.min.js',
        'node_modules/admin-lte/plugins/datepicker/bootstrap-datepicker.js',
        'node_modules/admin-lte/plugins/datepicker/locales/bootstrap-datepicker.pt.js',
        'node_modules/admin-lte/plugins/timepicker/bootstrap-timepicker.js',
        'node_modules/moment/min/moment.min.js',
        'node_modules/moment/locale/pt.js',
        'node_modules/admin-lte/dist/js/adminlte.min.js',
        /*Optionally, you can add Slimscroll and FastClick plugins.
          Both of these plugins are recommended to enhance the
          user experience.*/
        //'Scripts/ajax-forms.js',
        'Scripts/app.js'
    ],
    viewsToInjectScriptsAfterBody: [
        'Features/Shared/_Layout.cshtml'
    ],
    viewsToInjectScriptsAfterBodyFolder: 'Features/Shared',

    cssFiles: [
        'node_modules/admin-lte/plugins/bootstrap/css/bootstrap.min.css',
        'node_modules/admin-lte/dist/css/adminlte.min.css',
        'node_modules/admin-lte/plugins/font-awesome/css/font-awesome.min.css',
        'node_modules/admin-lte/plugins/select2/select2.min.css',
        'node_modules/admin-lte/plugins/datepicker/datepicker3.css',
        'node_modules/admin-lte/plugins/timepicker/bootstrap-timepicker.min.css'
        //'Content/css/Site.css'
    ],
    cssFilesRelease: [
        'Content/css/Site.css'
    ],
    viewsToInjectCss: [
        'Features/Shared/_Layout.cshtml'
    ],
    viewsToInjectCssFolder: 'Features/Shared',

    sassMainFile: 'Features/Site.scss',
    sassSrc: 'Features/',
    cssDist: 'Content/css',
    cssDistName: 'Site.css'
}
