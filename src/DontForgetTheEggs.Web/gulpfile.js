let gulp = require('gulp');
let config = require('./gulp.config');
let sass = require('gulp-sass');
let concat = require('gulp-concat');
let uglify = require('gulp-uglify');
let inject = require('gulp-inject');
let print = require('gulp-print').default;
let cleanCSS = require('gulp-clean-css');

const tasks = {
    injectJS: 'inject-js',
    injectJSRelease: 'inject-js-release',
    injectJSEndOfBody: 'inject-js-endofbody',
    injectJSEndOfBodyRelease: 'inject-js-endofbody-release',
    injectCss: 'inject-css',
    injectCssRelease: 'inject-css-release',
    buildSass: 'build-sass',
    buildSassRelease: 'build-sass-release',
    buildCssRelease: 'build-css-release'
}

gulp.task('default', [tasks.injectJS, tasks.injectCss]);
gulp.task('Debug', ['default']); // VS task

gulp.task('release', [tasks.injectJSRelease, tasks.injectCssRelease]);
gulp.task('Release', ['release']); // VS task

//TODO: there's no custom js, yet
gulp.task(tasks.injectJS, [tasks.injectJSEndOfBody]);
gulp.task(tasks.injectJSRelease, [tasks.injectJSEndOfBody]);

gulp.task(tasks.injectJSEndOfBody, function () {
    return gulp.src(config.viewsToInjectScriptsAfterBody)
        .pipe(inject(gulp.src(config.jsFilesAfterBody), { starttag: '<!-- inject:EndOfBodyApp:{{ext}} -->' }))
        .pipe(gulp.dest(config.viewsToInjectScriptsAfterBodyFolder));
});

gulp.task(tasks.injectCss/*, [tasks.buildSass]*/, function () {
    return gulp.src(config.viewsToInjectCss)
        .pipe(inject(gulp.src(config.cssFiles, { read: false }), { starttag: '<!-- inject:Styles:{{ext}} -->'}))
        .pipe(gulp.dest(config.viewsToInjectCssFolder));
});

gulp.task(tasks.injectCssRelease, [tasks.buildCssRelease], function () {
    return gulp.src(config.viewsToInjectCss)
        .pipe(inject(gulp.src(config.cssFilesRelease), {
            starttag: '<!-- inject:Styles:{{ext}} -->',
            transform: function (filePath, file) {
                return `<link rel="stylesheet" href="${filePath}?rev=${Date.now()}">`
            }
        }))
        .pipe(gulp.dest(config.viewsToInjectCssFolder));
});

/*
gulp.task(tasks.buildSass, function () {
    return gulp.src(config.sassMainFile)
        .pipe(sass({
            style: 'expanded',
            sourceComments: 'normal'
        }).on('error', sass.logError))
        .pipe(print())
        .pipe(gulp.dest(config.cssDist));
});*/

gulp.task(tasks.buildSassRelease, function () {
    return gulp.src(config.sassMainFile)
        .pipe(sass().on('error', sass.logError))
        .pipe(print())
        .pipe(gulp.dest(config.cssDist));
});

gulp.task(tasks.buildCssRelease, [tasks.buildSassRelease], function () {
    return gulp.src(config.cssFiles)
        .pipe(cleanCSS())
        .pipe(concat(config.cssDistName))
        .pipe(gulp.dest(config.cssDist));
});

gulp.task('watch', function () {
    //Watches the sass folder for all .scss and .sass files
    //If any file changes, run the sass task
    gulp.watch(config.sassSrc + '**/*.scss', [tasks.buildSass]);
});