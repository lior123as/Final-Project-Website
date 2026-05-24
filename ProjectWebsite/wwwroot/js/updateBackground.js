/**
 * futuristicBackground.js - 2026 High-Density Edition
 * Exported module for Blazor JS Interop.
 */

let canvas, ctx;
let width, height;
const fontSize = 14; // Smaller for higher density
const waves = [];
const lines = [];
let successWave = { x: 0, y: 0, radius: 0, active: false };

/**
 * Initializes the background and high-performance animation loop.
 * @param {string[]} customWords User-defined words to inject into text streams.
 */
export function initFuturisticBackground(customWords = ["CORE", "VOID", "USER", "SYNC"]) {
    canvas = document.getElementById('canvas-background');
    if (!canvas) return;
    ctx = canvas.getContext('2d', { alpha: false }); // Performance optimization

    const resize = () => {
        width = window.innerWidth;
        height = window.innerHeight;
        canvas.width = width;
        canvas.height = height;
    };
    window.addEventListener('resize', resize);
    resize();

    class AmbientWave {
        constructor() {
            this.x = Math.random() * width;
            this.y = Math.random() * height;
            this.radius = 0;
            this.maxRadius = Math.random() * 5000 + 200;
            this.opacity = 0.2;
        }
        update() {
            this.radius += 2.5;
            this.opacity -= 0.004;
        }
        draw() {
            ctx.beginPath();
            ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2);
            ctx.strokeStyle = `rgba(153, 51, 255, ${this.opacity})`; // Faint Purple
            ctx.lineWidth = 2;
            ctx.stroke();
        }
    }

    class GlitchLine {
        constructor(pos, isVertical) {
            this.isVertical = isVertical;
            this.pos = pos;
            this.offset = Math.random() * (isVertical ? height : width);
            this.speed = Math.random() * 4 + 2;
            this.chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ@#$%&*";
            this.word = customWords[Math.floor(Math.random() * customWords.length)];
            this.wordIdx = Math.floor(Math.random() * 20); // Random position in the stream
        }
        update() {
            this.offset += this.speed;
            const limit = this.isVertical ? height : width;
            if (this.offset > limit) {
                this.offset = -fontSize * 20;
                this.word = customWords[Math.floor(Math.random() * customWords.length)];
            }
        }
        draw() {
            ctx.font = `${fontSize}px 'Courier New', monospace`;

            for (let i = 0; i < 25; i++) {
                const drawX = this.isVertical ? this.pos : this.offset + (i * fontSize);
                const drawY = this.isVertical ? this.offset + (i * fontSize) : this.pos;

                // Determine character and word visibility
                const isWordChar = (i >= this.wordIdx && i < this.wordIdx + this.word.length);
                const char = isWordChar ? this.word[i - this.wordIdx] : this.chars[Math.floor(Math.random() * this.chars.length)];

                // Success Wave Calculation
                let color = isWordChar ? "rgba(0, 255, 204, 0.9)" : "rgba(0, 255, 204, 0.2)";
                let shadow = 0;

                if (successWave.active) {
                    const dist = Math.hypot(drawX - successWave.x, drawY - successWave.y);
                    const waveEdge = Math.abs(dist - successWave.radius);
                    if (waveEdge < 100) {
                        // High-visibility Green highlight
                        const intensity = 1 - (waveEdge / 100);
                        color = `rgba(0, 255, 0, ${intensity})`;
                        shadow = 15 * intensity; // Glow amount
                    }
                }

                // Apply Glow to text
                ctx.shadowBlur = shadow;
                ctx.shadowColor = "#00ff00";
                ctx.fillStyle = color;
                ctx.fillText(char, drawX, drawY);
                ctx.shadowBlur = 0; // Reset for next char
            }
        }
    }

    // High Density Initialization (Lines every 30px)
    for (let i = 0; i < width; i += 30) lines.push(new GlitchLine(i, true));
    for (let i = 0; i < height; i += 30) lines.push(new GlitchLine(i, false));

    function animate() {
        // Clear with slight fade for motion trails
        ctx.fillStyle = "rgba(0, 0, 0, 0.1)";
        ctx.fillRect(0, 0, width, height);

        // Success Wave Visual (Expanding ring)
        if (successWave.active) {
            successWave.radius += 5;
            ctx.beginPath();
            ctx.arc(successWave.x, successWave.y, successWave.radius, 0, Math.PI * 2);
            ctx.strokeStyle = `rgba(0, 255, 0, ${0.4 * (1 - successWave.radius / (width * 1.5))})`;
            ctx.lineWidth = 10;
            ctx.stroke();
            if (successWave.radius > width * 1.5) successWave.active = false;
        }

        // Ambient Purple Waves
        if (Math.random() < 0.03) waves.push(new AmbientWave());
        for (let i = waves.length - 1; i >= 0; i--) {
            waves[i].update();
            waves[i].draw();
            if (waves[i].opacity <= 0) waves.splice(i, 1);
        }

        // Glitch Lines
        lines.forEach(line => {
            line.update();
            line.draw();
        });

        requestAnimationFrame(animate);
    }

    animate();
}

/**
 * Activates the high-visibility success wave from the button position.
 * @param {string} elementId ID of the trigger element.
 */
export function triggerSuccessWave(elementId) {
    const el = document.getElementById(elementId);
    if (!el) return;
    const rect = el.getBoundingClientRect();
    successWave = {
        x: rect.left + rect.width / 2,
        y: rect.top + rect.height / 2,
        radius: 0,
        active: true
    };
}
