export function startFallingRects(canvasId) {
    const canvas = document.getElementById(canvasId);
    const ctx = canvas.getContext('2d');
    const colors = ['#00fff9', '#ff00c1','#000000','#ffffff'];
    const rectangles = [];// Array for falling rectangles
    const beams = []; // Array for upward glowing beams
    const random = (min, max) => {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min + 1)) + min;
    };

    const RECT_WIDTH = 192;
    const RECT_HEIGHT = 30;

    const resize = () => {
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
    };
    window.addEventListener('resize', resize);
    resize();

    class Beam {
        constructor(x, color) {
            this.x = x;
            this.y = canvas.height; // Start at bottom
            this.color = color;
            this.alpha = 0.5;
            this.speed = 25; // Speed of the beam shooting up
        }
        update() {
            this.y -= this.speed;
            this.alpha -= 0.005; // Fades out as it moves
        }
        draw() {
            ctx.save();
            ctx.globalAlpha = Math.max(0, this.alpha);
            ctx.shadowBlur = 20; // Intense glow effect
            ctx.shadowColor = this.color;
            ctx.fillStyle = this.color;
            // Draw a beam from the hit point all the way to the top
            ctx.fillRect(this.x, 0, RECT_WIDTH, canvas.height);
            ctx.restore();
        }
    }

    class FallingRect {
        constructor() { this.reset(true); }
        reset(randomY = false) {
            this.x = random(0, (canvas.width / RECT_WIDTH) - 1) * RECT_WIDTH;
            this.y = randomY ? Math.random() * canvas.height : -RECT_HEIGHT;
            this.speed = 1 + Math.random() * 3;
            this.color = colors[Math.floor(Math.random() * colors.length)];
            this.borderRadius = 8;
        }
        update() {
            this.y += this.speed;
            if (this.y >= canvas.height) {
                // Trigger beam before resetting
                beams.push(new Beam(this.x, this.color));
                this.reset();
            }
        }
        draw() {
            ctx.beginPath();
            ctx.fillStyle = this.color;


            ctx.roundRect(this.x, this.y, RECT_WIDTH, RECT_HEIGHT, this.borderRadius);

            ctx.fill();
            ctx.closePath();
        }
    }

    for (let i = 0; i < 8; i++) rectangles.push(new FallingRect());


    function animate() {
        // 1. SAVE the current context state
        ctx.save();

        // 2. USE "destination-out" to fade everything out
        // Higher globalAlpha = shorter trails; Lower = longer trails
        ctx.globalAlpha = 0.15;
        ctx.globalCompositeOperation = "destination-out";
        ctx.fillStyle = "#000"; // Color doesn't matter for destination-out
        ctx.fillRect(0, 0, canvas.width, canvas.height);

        // 3. RESTORE to standard drawing mode for the new frame
        ctx.restore();

        // Draw beams (behind falling rects)
        for (let i = beams.length - 1; i >= 0; i--) {
            beams[i].update();
            beams[i].draw();
            if (beams[i].alpha <= 0) beams.splice(i, 1);
        }

        // Draw falling rectangles
        rectangles.forEach(r => {
            r.update();
            r.draw();
        });

        requestAnimationFrame(animate);
    }


    animate();
}
