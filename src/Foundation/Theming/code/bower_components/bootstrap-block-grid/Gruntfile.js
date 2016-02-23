module.exports = function(grunt) {

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        sass: {
            distMin: {
                options: {                       // Target options
                    style: 'compressed',
                    sourcemap: 'none'
                },
                files: {
                    'dist/bootstrap3-block-grid.min.css': 'src/bootstrap3-block-grid.scss',
                    'dist/bootstrap4-block-grid.min.css': 'src/bootstrap4-block-grid.scss'
                }
            },
            dist: {
                options: {                       // Target options
                    style: 'expanded',
                    sourcemap: 'none'
                },
                files: {
                    'dist/bootstrap3-block-grid.css': 'src/bootstrap3-block-grid.scss',
                    'dist/bootstrap4-block-grid.css': 'src/bootstrap4-block-grid.scss'
                }
            }

        },
        watch: {
            minifySCSS: {
                files: [
                    'src/*.scss'
                ],
                tasks: ['sass'],
                options: {
                    spawn: true,
                },
            }
        },
    });

    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-sass');

    grunt.registerTask('default', ['watch']);

};

