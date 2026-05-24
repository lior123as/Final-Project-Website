/**
 * warningBackground.js - Warning/Hazard Theme
 */
let canvas, ctx, width, height;
let drops = [];
const fontSize = 16;

export function initWarningBackground(warningWords) {
    canvas = document.getElementById('canvas-background');
    if (!canvas) return;
    ctx = canvas.getContext('2d');

    resize();
    window.addEventListener('resize', resize);

    const columns = Math.floor(width / fontSize);
    drops = new Array(columns).fill(1);

    function draw() {
        // Semi-transparent black to create trailing effect
        ctx.fillStyle = 'rgba(0, 0, 0, 0.05)';
        ctx.fillRect(0, 0, width, height);

        // Warning Colors (Yellow/Amber/Red)
        ctx.fillStyle = '#ffcc00';
        ctx.font = fontSize + 'px Courier New';

        for (let i = 0; i < drops.length; i++) {
            // Randomly pick a warning character or word
            const text = warningWords[Math.floor(Math.random() * warningWords.length)];
            ctx.fillText(text, i * fontSize, drops[i] * fontSize);

            if (drops[i] * fontSize > height && Math.random() > 0.975) {
                drops[i] = 0;
            }
            drops[i]++;
        }
        requestAnimationFrame(draw);
    }
    draw();
}

function resize() {
    width = canvas.width = window.innerWidth;
    height = canvas.height = window.innerHeight;
}

export function triggerWarningWave(elementId) {
    // Visual feedback for clicking the warn button
    const btn = document.getElementById(elementId);
    if (btn) {
        btn.style.boxShadow = "0 0 50px #ff3300";
        setTimeout(() => btn.style.boxShadow = "0 0 15px #ffcc00", 300);
    }
}
