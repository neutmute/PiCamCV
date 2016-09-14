/// <binding AfterBuild='postBuild' Clean='clean' ProjectOpened='projectOpen' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        clean: {
            js: ["typescript/**/*.js", "assets/app-ts.*"]
        },

        ts: {
            default: {
                src: ["typescript/**/*.ts", "!node_modules/**"],
                out: 'assets/app-ts.js'
            }
        },
        watch: {
            typescript: {
                files: ['typescript/**/*.ts'],
                tasks: ['ts']
            }
        },
        typings: {
            install: {}
        }

    });

    grunt.loadNpmTasks("grunt-ts");
    grunt.loadNpmTasks("grunt-typings");
    grunt.loadNpmTasks("grunt-contrib-watch");
    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.registerTask("default", ["ts"]);
    
    grunt.registerTask('postBuild', []); // shouldnt need to compile Typescript since watch will do it
    grunt.registerTask('projectOpen', ['typings', 'ts', 'watch']);
};