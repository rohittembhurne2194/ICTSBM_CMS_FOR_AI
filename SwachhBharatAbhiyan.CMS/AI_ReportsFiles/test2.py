import cv2
import numpy as np
import os

folder = 'C:/Users/user/Downloads'
image_paths = []
for path, subdirs, files in os.walk(folder):
    for filename in files:
        f = os.path.join(path, filename)
        if f.endswith((".jpg", ".png", ".JPG", ".PNG", ".jpeg", ".JPEG")):
            image_paths.append(f)

d = 0                                               # Final image counter
e = 0                                               # Single image counter
back = np.ones((1080, 1920, 3), np.uint8) * 255     # Background
result = back.copy()                                # Final image
for i, image in enumerate(image_paths):

    # Read image
    image = cv2.imread(image)
    h, w = image.shape[:2]

    # First two single images: Enforce subimage with h_max = 480 and w_max = 900
    if e <= 1:
        r = 900.0 / w
        dim = (900, int(h * r))
        if dim[1] > 480:
            r = 480.0 / h
            dim = (int(w * r), 480)
        resized = cv2.resize(image, dim)
        hr, wr = resized.shape[:2]
        x_off = 40
        if e == 0:
            y_off = 40
        else:
            y_off = 560

    # Third single image: Enforce subimage with h_max = 1000 and w_max = 900
    else:
        r = 900.0 / w
        dim = (900, int(h * r))
        if dim[1] > 1000:
            r = 1000.0 / h
            dim = (int(w * r), 1000)
        resized = cv2.resize(image, dim)
        hr, wr = resized.shape[:2]
        x_off, y_off = 980, 40

    # Add single image to final image
    result[y_off:y_off + hr, x_off:x_off + wr] = resized

    # Increment single image counter
    e += 1

    # After three single images: Write final image; start new final image
    if (e == 3) or (i == (len(image_paths) - 1)):
        cv2.imwrite('resized_centered_%d.jpg' % d, result)
        result = back.copy()
        d += 1
        e = 0
