#!/usr/bin/env node
/**
 * Breweryinator Theme Builder
 * Compiles the custom Bootstrap SCSS theme and copies outputs to the Blazor WASM wwwroot.
 */

const { execSync } = require('child_process');
const path = require('path');
const fs = require('fs');

const rootDir = __dirname;
const scssEntry = path.join(rootDir, 'scss', 'breweryinator.scss');
const outDir = path.join(rootDir, '..', 'Breweryinator.Web', 'wwwroot', 'css');
const outCss = path.join(outDir, 'breweryinator.min.css');

const iconsSource = path.join(rootDir, 'node_modules', 'bootstrap-icons', 'font');
const iconsDestDir = path.join(outDir, 'bootstrap-icons');

// Ensure output directory exists
fs.mkdirSync(outDir, { recursive: true });
fs.mkdirSync(iconsDestDir, { recursive: true });

// Compile SCSS → minified CSS using the sass CLI from node_modules
const sassExe = path.join(rootDir, 'node_modules', '.bin', 'sass');
const loadPath = path.join(rootDir, 'node_modules');
console.log('🍺 Compiling Bootstrap theme...');
execSync(`"${sassExe}" "${scssEntry}" "${outCss}" --style=compressed --no-source-map --load-path="${loadPath}"`, {
    stdio: 'inherit',
    cwd: rootDir
});
console.log(`✅ Theme CSS written to ${outCss}`);

// Copy Bootstrap Icons fonts and CSS
const iconsCssSource = path.join(iconsSource, 'bootstrap-icons.min.css');
const iconsFontSource = path.join(iconsSource, 'fonts');

if (fs.existsSync(iconsCssSource)) {
    // Adjust font paths in the CSS so they resolve relative to wwwroot/css/bootstrap-icons/
    let iconsCSS = fs.readFileSync(iconsCssSource, 'utf8');
    iconsCSS = iconsCSS.replace(/url\("fonts\//g, 'url("bootstrap-icons/fonts/');
    fs.writeFileSync(path.join(outDir, 'bootstrap-icons.min.css'), iconsCSS);
    console.log('✅ Bootstrap Icons CSS written');
}

if (fs.existsSync(iconsFontSource)) {
    const fontsOutDir = path.join(iconsDestDir, 'fonts');
    fs.mkdirSync(fontsOutDir, { recursive: true });
    for (const file of fs.readdirSync(iconsFontSource)) {
        fs.copyFileSync(
            path.join(iconsFontSource, file),
            path.join(fontsOutDir, file)
        );
    }
    console.log('✅ Bootstrap Icons fonts copied');
}

console.log('🎉 Theme build complete!');
