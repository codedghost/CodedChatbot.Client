/// <binding BeforeBuild='sass' />
var gulp = require("gulp"),
    sass = require("gulp-sass");

// other content removed

gulp.task("sass", function () {
    return gulp.src('Styles/*.scss')
        .pipe(sass())
        .pipe(gulp.dest('wwwroot/css'));
});